using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(Temperature))]
public class TemperatureDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var temperature_element = new VisualElement();
        temperature_element.style.flexDirection = FlexDirection.Row;

        var temperatureValue = new PropertyField(property.FindPropertyRelative("value"), "Temperature");
        temperatureValue.style.flexGrow = 1;
        temperature_element.Add(temperatureValue);

        var unitProperty = new PropertyField(property.FindPropertyRelative("unit"));
        temperature_element.Add(unitProperty);

        container.Add(temperature_element);

        return container;
    }
}

public class GeneralScalarUnitDrawer<T_Unit> : PropertyDrawer where T_Unit : struct, Enum
{
    public VisualElement CreateGUI<T>(SerializedProperty property) where T : struct, IPhysicalValue<T_Unit>
    {
        var container = new VisualElement();

        var temperature_element = new VisualElement();
        temperature_element.style.flexDirection = FlexDirection.Row;

        var temperatureValue = new PropertyField(property.FindPropertyRelative("value"), "Temperature");
        temperatureValue.style.flexGrow = 1;
        temperature_element.Add(temperatureValue);

        var unitProperty = new PropertyField(property.FindPropertyRelative("unit"));
        temperature_element.Add(unitProperty);

        container.Add(temperature_element);

        return container;
    }
}