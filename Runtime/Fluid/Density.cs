using System;
using System.Collections.Generic;


[Serializable]
public struct Density : IPhysicalValue<Density.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    // TODO
    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.GrammPerCubicCentimeter,     new Quantity(0.001f, 0) },
        { Units.KilogramPerCubicDecimeter,     new Quantity(0.001f, 0) },
        { Units.KilogramPerCubicMeter,     new Quantity(1f, 0) },
    };

    public Density(float value, Units unit) { this.value = value; this.unit = unit; }
    public Density(float value) : this(value, Units.KilogramPerCubicMeter) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Density ConvertDensity(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Density ToDensity(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units { GrammPerCubicCentimeter, GrammPerDecimeter, KilogramPerCubicDecimeter, KilogramPerCubicMeter, }
}