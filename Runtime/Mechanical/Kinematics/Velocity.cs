using System;
using System.Collections.Generic;

[Serializable]
public struct Velocity : IPhysicalValue<Velocity.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.MeterPerSecound,     new Quantity(1, 0) },
        { Units.KilometerPerHour,     new Quantity(1/3.6f, 0) },
    };

    public Velocity(float value, Units unit) { this.value = value; this.unit = unit; }
    public Velocity(float value) : this(value, Units.MeterPerSecound) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Velocity ConvertVelocity(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Velocity ToVelocity(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);
    public static Acceleration operator /(Velocity s, Duration t) => new(s.To(Units.MeterPerSecound) / t.To(Duration.Units.Secound), Acceleration.Units.MeterPerSquareSecound);
    public static Impulse operator /(Mass m, Velocity v) => new(m.To(Mass.Units.Kilogramm) / v.To(Velocity.Units.MeterPerSecound), Impulse.Units.NewtonSecond);

    public enum Units { MeterPerSecound, KilometerPerHour }
}
