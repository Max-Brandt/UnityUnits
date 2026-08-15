using System;
using System.Collections.Generic;

[Serializable]
public struct Acceleration : IPhysicalValue<Acceleration.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.MeterPerSquareSecound,     new Quantity(1, 0) },
    };

    public Acceleration(float value, Units unit) { this.value = value; this.unit = unit; }
    public Acceleration(float value) : this(value, Units.MeterPerSquareSecound) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Acceleration ConvertAcceleration(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Acceleration ToAcceleration(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Velocity operator *(Acceleration a, Duration t) => new(a.To(Units.MeterPerSquareSecound) * t.To(Duration.Units.Secound), Velocity.Units.MeterPerSecound);
    public static Jerk operator /(Acceleration a, Duration t) => new(a.To(Units.MeterPerSquareSecound) * t.To(Duration.Units.Secound), Jerk.Units.MeterPerCubicSecound);

    public enum Units { MeterPerSquareSecound }
}
