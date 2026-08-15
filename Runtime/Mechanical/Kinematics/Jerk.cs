using System;
using System.Collections.Generic;

[Serializable]
public struct Jerk : IPhysicalValue<Jerk.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.MeterPerCubicSecound,     new Quantity(1, 0) },
    };

    public Jerk(float value, Units unit) { this.value = value; this.unit = unit; }
    public Jerk(float value) : this(value, Units.MeterPerCubicSecound) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Jerk ConvertJerk(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Jerk ToJerk(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Acceleration operator *(Jerk j, Duration t) => new(j.To(Units.MeterPerCubicSecound) * t.To(Duration.Units.Secound), Acceleration.Units.MeterPerSquareSecound);

    public enum Units { MeterPerCubicSecound }
}
