
//using System;
//using UnityEngine;
//using UnitsNet.Units;

//[Serializable]
//public struct Impulse
//{
//    [SerializeField] float m_Value;
//    [SerializeField] ImpulseUnit m_Unit;

//    public Impulse(float value, ImpulseUnit unit = ImpulseUnit.NewtonSecond)
//    {
//        m_Value = value;
//        m_Unit = unit;
//    }

//    public static implicit operator Impulse(float value) => new(value, ImpulseUnit.NewtonSecond);

//    public readonly float Value => m_Value;
//    public readonly ImpulseUnit unit => m_Unit;

//    public readonly float InNewtonSecond => In(ImpulseUnit.NewtonSecond);
//    public readonly float In(ImpulseUnit target) => (float)new UnitsNet.Impulse(m_Value, m_Unit).As(target);

//    public static float operator -(Impulse a, Impulse b) => a.InNewtonSecond - b.InNewtonSecond;
//    public static float operator +(Impulse a, Impulse b) => a.InNewtonSecond + b.InNewtonSecond;
//}


using System;
using System.Collections.Generic;

[Serializable]
public struct HeatFlux : IPhysicalValue<HeatFlux.Units>
{
    public float value;
    public Units unit;

    readonly Dictionary<Units, Quantity> IPhysicalValue<Units>.Converters => _converters;

    private static readonly Dictionary<Units, Quantity> _converters = new()
    {
        { Units.WattPerSquareMeter,     new Quantity(1, 0) }
    };

    public HeatFlux(float value, Units unit) { this.value = value; this.unit = unit; }
    public HeatFlux(float value) : this(value, Units.WattPerSquareMeter) { }
    public static float Convert(float value, Units From, Units To) => IPhysicalValue<Units>.Convert(value, From, To, _converters);
    public static HeatFlux ConvertHeatFlux(float value, Units From, Units To) => new(IPhysicalValue<Units>.Convert(value, From, To, _converters), To);
    public readonly float To(Units To) => IPhysicalValue<Units>.Convert(value, unit, To, _converters);
    public readonly HeatFlux ToHeatFlux(Units To) => new(IPhysicalValue<Units>.Convert(value, unit, To, _converters), To);

    public enum Units
    {
        WattPerSquareMeter,
    }
}

