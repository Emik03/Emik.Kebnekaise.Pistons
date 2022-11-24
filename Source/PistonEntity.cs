// <copyright file="PistonEntity.cs" company="Emik">
// Copyright (c) Emik. This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>
namespace Emik.Kebnekaise.Pistons;

/// <summary>A modded solid that moves when touched.</summary>
[CustomEntity($"KebnekaiseHelper/{nameof(PistonEntity)}")]
public sealed class PistonEntity : Solid
{
    /// <summary>Gets a dictionary that contains a key-value mapping of every ease in <see cref="Eases"/>.</summary>
    [Pure]
    public static IDictionary<string, Easer> Eases { get; } =
        typeof(Ease).GetFields().ToDictionary(x => x.Name, x => (Easer)x.GetValue(null));

    /// <summary>Initializes a new instance of the <see cref="PistonEntity"/> class.</summary>
    /// <param name="data">The entity data containing the tile data and ease type.</param>
    /// <param name="offset">The destination of the block.</param>
    public PistonEntity(EntityData data, Vector2 offset)
        : base(data.Position + offset, data.Width, data.Height, false)
    {
        const int PixelsPerTile = 8;
        const char DefaultCharType = '3';

        var easeData = data.Attr("ease", nameof(Linear));
        var tileData = data.Char("tiletype", DefaultCharType);
        var timeData = data.Float("time", 1);

        SurfaceSoundIndex = SurfaceIndex.TileToIndex.Nth(tileData) is var c && c is '\0' ? c : DefaultCharType;

        int width = data.Width / PixelsPerTile,
            height = data.Height / PixelsPerTile;

        var box = GFX.FGAutotiler.GenerateBox(tileData, width, height).TileGrid;
        var dest = data.Nodes[0] + offset;
        var ease = Eases.Nth(easeData) ?? Linear;

        Coroutine move = new(Move(ease, dest, timeData));

        Add(box);
        Add(move);
    }

    /// <summary>An enumerator for updating the location of this object.</summary>
    /// <param name="ease">The ease to use.</param>
    /// <param name="end">The location to move to.</param>
    /// <param name="time">The amount of time needed for moving.</param>
    /// <returns>
    /// An <see cref="IEnumerator{T}"/> object that updates the location
    /// of itself when <see cref="IEnumerator{T}.MoveNext"/> is invoked.
    /// <see cref="IEnumerator{T}.Current"/> is always <see langword="null"/>.
    /// </returns>
    [Pure]
    public IEnumerator<object?> Move(Easer ease, Vector2 end, float time)
    {
    start:

        while (!HasPlayerRider())
            yield return null;

        var start = Position;
        var f = 0f;

        while (f < time)
        {
            f += Engine.DeltaTime;
            var next = ease(f);
            var lerp = Vector2.Lerp(start, end, next);
            MoveTo(lerp);

            yield return null;
        }

        MoveTo(end);
        end = start;
        goto start;
        // ReSharper disable once IteratorNeverReturns
    }
}
