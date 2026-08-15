using System;
using System.Collections.Generic;

[Serializable]
public struct Impulse : IPhysicalValue<Impulse.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.NewtonSecond,     new Quantity(1, 0) }
    };

    public Impulse(float value, Units unit) { this.value = value; this.unit = unit; }
    public Impulse(float value) : this(value, Units.NewtonSecond) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Impulse ConvertImpulse(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Impulse ToImpulse(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units { NewtonSecond }
}
