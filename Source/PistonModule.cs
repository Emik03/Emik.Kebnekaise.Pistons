// SPDX-License-Identifier: MPL-2.0
namespace Emik.Kebnekaise.Pistons;

/// <summary>This module definition is required for the mod to load.</summary>
[CLSCompliant(false)]
public sealed class PistonModule : EverestModule
{
    static PistonModule? s_instance;

    /// <summary>Initializes a new instance of the <see cref="PistonModule" /> class.</summary>
    public PistonModule() => s_instance = this;

    /// <summary>Gets the singleton instance.</summary>
    [Pure]
    public static PistonModule Instance => s_instance ??= new();

    /// <inheritdoc />
    public override void Load() { }

    /// <inheritdoc />
    public override void Unload() { }
}
