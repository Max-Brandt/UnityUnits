using System;
using System.Collections.Generic;


[Serializable]
public struct Area : IPhysicalValue<Area.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.SquareKilometer,     new Quantity(1e6f, 0) },
        { Units.Hectare,     new Quantity(1e4f, 0) },
        { Units.SquareMeter,     new Quantity(1, 0) },
        { Units.SquareDecimeter,     new Quantity(1e-2f, 0) },
        { Units.SquareCentimeter,     new Quantity(1e-4f, 0) },
        { Units.SquareMillimeter,     new Quantity(1e-6f, 0) },
    };

    public Area(float value, Units unit) { this.value = value; this.unit = unit; }
    public Area(float value) : this(value, Units.SquareMeter) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static Area ConvertArea(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly Area ToArea(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public static Length operator /(Area A, Length l) => new(A.To(Area.Units.SquareMeter) / l.To(Length.Units.Meter), Length.Units.Meter);
    public static Volume operator *(Area A, Length l) => new(A.To(Area.Units.SquareMeter) *l.To(Length.Units.Meter), Volume.Units.CubicKilometer);

    public enum Units { SquareKilometer, SquareMeter, SquareMillimeter, SquareCentimeter, SquareDecimeter, Hectare ,}
}
