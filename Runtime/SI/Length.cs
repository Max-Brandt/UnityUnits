
using System;
using System.Collections.Generic;

[Serializable]
public struct Length : IPhysicalValue<Length.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Kilometer,     new Quantity(1000f, 0) },
        { Units.Meter,     new Quantity(1, 0) },
        { Units.Centimeter,    new Quantity(0.01f, 0f) },
        { Units.Millimeter,    new Quantity(0.001f, 0f) },
        { Units.Micrometer,    new Quantity(0.000001f, 0f) },
        { Units.Nanometer,    new Quantity(0.000000001f, 0f) },
    };

    public Length(float value, Units unit) { this.value = value; this.unit = unit; }
    public Length(float value) : this(value, Units.Meter) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Length ConvertLength(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Length ToLength(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);
    public static Velocity operator /(Length s, Duration t) => new (s.To(Units.Meter) / t.To(Duration.Units.Secound), Velocity.Units.MeterPerSecound);

    public enum Units { Kilometer, Meter, Centimeter, Millimeter , Micrometer, Nanometer}
}
