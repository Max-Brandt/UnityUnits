using System;
using System.Collections.Generic;


[Serializable]
public struct Volume : IPhysicalValue<Volume.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.CubicKilometer, new Quantity(1e9f, 0) },
        { Units.CubicMeter, new Quantity(1, 0) },
        { Units.CubicDecimeter, new Quantity(1e-3f, 0) },
        { Units.Liter, new Quantity(1e-3f, 0) },
        { Units.CubicMillimeter, new Quantity(1e-9f, 0) },
        { Units.CubicCentimeter, new Quantity(1e-6f, 0) },
    };

    public Volume(float value, Units unit) { this.value = value; this.unit = unit; }
    public Volume(float value) : this(value, Units.CubicMeter) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Volume ConvertVolume(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Volume ToVolume(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Density operator /(Mass m, Volume V) => new(m.To(Mass.Units.Kilogramm) / V.To(Volume.Units.CubicMeter), Density.Units.KilogramPerCubicMeter);

    public static Area operator /(Volume V, Length l) => new(V.To(Volume.Units.CubicMeter)/l.To(Length.Units.Meter), Area.Units.SquareMeter);

    
    public enum Units { 
        CubicKilometer, 
        CubicMeter, 
        Liter, 
        CubicDecimeter, 
        CubicCentimeter,
        CubicMillimeter
    }
}
