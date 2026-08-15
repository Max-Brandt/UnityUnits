using System;
using System.Collections.Generic;

[Serializable]
public struct ThermalResistance : IPhysicalValue<ThermalResistance.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.SquareMeterKelvinPerKilowatt,     new Quantity(1, 0) }
    };

    public ThermalResistance(float value, Units unit) { this.value = value; this.unit = unit; }
    public ThermalResistance(float value) : this(value, Units.SquareMeterKelvinPerKilowatt) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static ThermalResistance ConvertThermalResistance(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly ThermalResistance ToThermalResistance(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units
    {
        SquareMeterKelvinPerKilowatt
    }
}

