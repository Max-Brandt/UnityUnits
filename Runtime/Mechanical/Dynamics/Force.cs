using System;
using System.Collections.Generic;

[Serializable]
public struct Force : IPhysicalValue<Force.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Newton,     new Quantity(1, 0) },
        { Units.KiloNewton,     new Quantity(1000, 0) },
    };

    public Force(float value, Units unit) { this.value = value; this.unit = unit; }
    public Force(float value) : this(value, Units.Newton) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Force ConvertForce(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Force ToForce(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Torque operator *(Force F, Length l) => new(F.To(Units.Newton) * l.To(Length.Units.Meter), Torque.Units.NewtonMeter);


    public enum Units { Newton, KiloNewton }
}
