using System;
using System.Collections.Generic;


[Serializable]
public struct Angle : IPhysicalValue<Angle.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Degree,     new Quantity((float)Math.PI / 180f, 0) },
        { Units.Radian,     new Quantity(1, 0) },
        { Units.Arcsecond,     new Quantity((float)Math.PI / (3600 * 180), 0) },
        { Units.Arcminute,     new Quantity((float)Math.PI / (60 * 180), 0) },
    };

    public Angle(float value, Units unit) { this.value = value; this.unit = unit; }
    public Angle(float value) : this(value, Units.Radian) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Angle ConvertAngle(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Angle ToAngle(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units
    {
        Degree,
        Radian,
        Arcminute,
        Arcsecond,
    }
}
