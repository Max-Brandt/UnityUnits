using System;
using System.Collections.Generic;

public interface IPhysicalValue<TUnit> where TUnit : struct, Enum
{
    protected Dictionary<TUnit, Quantity> Converters { get; }

    public static float Convert(float value, TUnit from, TUnit to, Dictionary<TUnit, Quantity> converters)
    {
        if (EqualityComparer<TUnit>.Default.Equals(from, to))
            return value;
        if (!converters.TryGetValue(from, out var fromQ))
            throw new Exception($"Unit {from} not available");
        if (!converters.TryGetValue(to, out var toQ))
            throw new Exception($"Unit {to} not available");
        return toQ.ConvertBack(fromQ.Convert(value));
    }
}