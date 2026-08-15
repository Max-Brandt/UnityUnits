
using System;
using System.Collections.Generic;

[Serializable]
public struct Torque : IPhysicalValue<Torque.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.NewtonMeter,     new Quantity(1, 0) },
        { Units.KiloNewtonMeter,     new Quantity(1000, 0) },
    };

    public Torque(float value, Units unit) { this.value = value; this.unit = unit; }
    public Torque(float value) : this(value, Units.NewtonMeter) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Torque ConvertTorque(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Torque ToTorque(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Force operator /(Torque T, Length l) => new(T.To(Units.NewtonMeter) * l.To(Length.Units.Meter), Force.Units.Newton);

    public enum Units { NewtonMeter, KiloNewtonMeter }
}
