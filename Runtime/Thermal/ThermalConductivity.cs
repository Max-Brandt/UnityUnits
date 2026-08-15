using System;
using System.Collections.Generic;

[Serializable]
public struct ThermalConductivity : IPhysicalValue<ThermalConductivity.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.WattPerMeterKelvin,     new Quantity(1, 0) }
    };

    public ThermalConductivity(float value, Units unit) { this.value = value; this.unit = unit; }
    public ThermalConductivity(float value) : this(value, Units.WattPerMeterKelvin) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static ThermalConductivity ConvertThermalConductivity(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly ThermalConductivity ToThermalConductivity(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units
    {
        WattPerMeterKelvin,
    }
}

