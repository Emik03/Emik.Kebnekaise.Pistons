// <copyright file="PistonModule.cs" company="Emik">
// Copyright (c) Emik. This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>
namespace Emik.Kebnekaise.Piston;

/// <summary>This module definition is required for the mod to load.</summary>
public sealed class PistonModule : EverestModule
{
    static PistonModule? s_instance;

    /// <summary>Initializes a new instance of the <see cref="PistonModule"/> class.</summary>
    public PistonModule() => s_instance = this;

    /// <summary>Gets the singleton instance.</summary>
    [Pure]
    public static PistonModule Instance => s_instance ??= new();

    /// <inheritdoc />
    public override void Load() { }

    /// <inheritdoc />
    public override void Unload() { }
}
