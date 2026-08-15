
using System;
using System.Collections.Generic;

[Serializable]
public struct Duration : IPhysicalValue<Duration.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Day,     new Quantity(3600*24, 0) },
        { Units.Hour,     new Quantity(3600, 0) },
        { Units.Minute,    new Quantity(60, 0f) },
        { Units.Secound,    new Quantity(1f, 0f) },
        { Units.Millisecound,    new Quantity(0.001f, 0f) },
        { Units.Microsecound,    new Quantity(0.000001f, 0f) },
    };

    public Duration(float value, Units unit) { this.value = value; this.unit = unit; }
    public Duration(float value) : this(value, Units.Secound) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Duration ConvertDuration(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Duration ToDuration(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units { Day, Hour, Minute, Secound, Millisecound, Microsecound }
}
