using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;

namespace RainbowArt.CleanFlatUI
{
    [CustomEditor(typeof(TooltipSpecial))]
    public class TooltipSpecialEditor : Editor
    {
        SerializedProperty description;
        SerializedProperty animator;
        SerializedProperty origin;
        SerializedProperty offset;
        bool showMargins = false;

        protected virtual void OnEnable()
        {
            description = serializedObject.FindProperty("description");
            animator = serializedObject.FindProperty("animator");
            origin = serializedObject.FindProperty("origin");
            offset = serializedObject.FindProperty("offset");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(description);
            EditorGUILayout.PropertyField(animator);
            EditorGUILayout.PropertyField(origin);
            EditorGUILayout.PropertyField(offset);
            serializedObject.ApplyModifiedProperties();
            if (description.objectReferenceValue != null)
            {
                var d = description.objectReferenceValue as TextMeshProUGUI;
                showMargins = EditorGUILayout.Foldout(showMargins, "Margins");
                if (showMargins)
                {
                    float x = EditorGUILayout.FloatField("Left", d.margin.x);
                    float y = EditorGUILayout.FloatField("Top", d.margin.y);
                    float z = EditorGUILayout.FloatField("Right", d.margin.z);
                    float w = EditorGUILayout.FloatField("Bottom", d.margin.w);
                    Vector4 newMargins = new(x, y, z, w);
                    if (d.margin != newMargins)
                    {
                        d.margin = newMargins;
                        // d.ForceMeshUpdate();
                        d.enabled = false;
                        d.enabled = true;
                        (target as TooltipSpecial).UpdateHeight();
                    }

                }
            }
        }
    }
}
