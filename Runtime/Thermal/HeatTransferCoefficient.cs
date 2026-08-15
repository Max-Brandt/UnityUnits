using System;
using System.Collections.Generic;

[Serializable]
public struct HeatTransferCoefficient : IPhysicalValue<HeatTransferCoefficient.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.WattPerSquareMeterKelvin,     new Quantity(1, 0) }
    };

    public HeatTransferCoefficient(float value, Units unit) { this.value = value; this.unit = unit; }
    public HeatTransferCoefficient(float value) : this(value, Units.WattPerSquareMeterKelvin) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static HeatTransferCoefficient ConvertHeatTransferCoefficient(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly HeatTransferCoefficient ToHeatTransferCoefficient(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units
    {
        WattPerSquareMeterKelvin,
    }
}

