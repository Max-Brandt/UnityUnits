
using System;
using System.Collections.Generic;

[Serializable]
public struct Temperature : IPhysicalValue<Temperature.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.Kelvin,     new Quantity(1, 0) },
        { Units.Celsius,    new Quantity(1, 273.15f) },
        { Units.Fahrenheit, new Quantity(1/1.8f, -32/1.8f + 273.15f) }
    };

    public Temperature(float value, Units unit) { this.value = value; this.unit = unit; }
    public Temperature(float value) : this(value, Units.Kelvin) { }

    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Temperature ConvertTemperature(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);

    public readonly Temperature ToTemperature(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units { Celsius, Kelvin, Fahrenheit }
}
