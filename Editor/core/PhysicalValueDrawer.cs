using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public abstract class PhysicalValueDrawer : PropertyDrawer
{
    private const float UnitFieldMinWidth = 90f;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var valueProp = property.FindPropertyRelative("value");
        var unitProp = property.FindPropertyRelative("unit");

        var row = new VisualElement();
        row.style.flexDirection = FlexDirection.Row;

        var valueField = new PropertyField(valueProp, property.displayName);
        valueField.style.flexGrow = 1;
        row.Add(valueField);

        var unitField = new PopupField<string>(new List<string>(unitProp.enumDisplayNames), unitProp.enumValueIndex);
        unitField.style.minWidth = UnitFieldMinWidth;
        row.Add(unitField);

        unitField.RegisterValueChangedCallback(evt =>
        {
            var fromIndex = Array.IndexOf(unitProp.enumDisplayNames, evt.previousValue);
            var toIndex = Array.IndexOf(unitProp.enumDisplayNames, evt.newValue);
            if (fromIndex < 0 || toIndex < 0 || fromIndex == toIndex)
                return;

            var converted = ConvertValue(property, valueProp.floatValue, fromIndex, toIndex);

            unitProp.enumValueIndex = toIndex;
            valueProp.floatValue = converted;
            property.serializedObject.ApplyModifiedProperties();
        });

        row.TrackPropertyValue(unitProp, p => unitField.SetValueWithoutNotify(p.enumDisplayNames[p.enumValueIndex]));

        return row;
    }

    private static float ConvertValue(SerializedProperty property, float value, int fromIndex, int toIndex)
    {
        var quantityType = property.boxedValue.GetType();
        var unitType = quantityType.GetNestedType("Units");
        if (unitType == null)
            return value;

        var convert = quantityType.GetMethod("Convert", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(float), unitType, unitType }, null);
        if (convert == null)
            return value;

        var unitValues = Enum.GetValues(unitType);
        var from = unitValues.GetValue(fromIndex);
        var to = unitValues.GetValue(toIndex);
        return (float)convert.Invoke(null, new[] { (object)value, from, to });
    }
}
