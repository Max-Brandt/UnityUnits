
using System;
using System.Collections.Generic;


[Serializable]
public struct Mass : IPhysicalValue<Mass.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Ton,     new Quantity(1000, 0) },
        { Units.Kilogramm,     new Quantity(1, 0) },
        { Units.Gramm,     new Quantity(0.001f, 0) },
        { Units.Milligramm,     new Quantity(0.000_001f, 0) }
    };

    public Mass(float value, Units unit) { this.value = value; this.unit = unit; }
    public Mass(float value) : this(value, Units.Kilogramm) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Mass ConvertMass(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Mass ToMass(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);
    public static Force operator *(Mass m, Acceleration a) => new(m.To(Mass.Units.Kilogramm) * a.To(Acceleration.Units.MeterPerSquareSecound), Force.Units.Newton);
    public static Density operator /(Mass m, Volume V) => new(m.To(Mass.Units.Kilogramm) / V.To(Volume.Units.CubicMeter), Density.Units.KilogramPerCubicMeter);

    public enum Units { Ton, Kilogramm, Gramm, Milligramm }
}