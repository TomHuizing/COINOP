using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDateTime))]
public class SerializableDateTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        SerializedProperty ticksProp = property.FindPropertyRelative("ticks");
        DateTime date = new(ticksProp.longValue);
        string dateString = date.ToString("yyyy-MM-dd HH:mm:ss");
        EditorGUI.BeginChangeCheck();
        dateString = EditorGUI.TextField(position, label, dateString);
        if (EditorGUI.EndChangeCheck() && DateTime.TryParse(dateString, out var parsedDate))
        {
            ticksProp.longValue = parsedDate.Ticks;
        }
        EditorGUI.EndProperty();
    }
}