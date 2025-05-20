using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TextInputPopup : EditorWindow
{
    
    private string inputText = "";
    private string message = "Please enter some text:";
    private string textLabel = "Input:";
    private Action<string> onSubmit;
    private Action onCancel;

    public static void Show(string title, string message, string textLabel, Action<string> onSubmit, Action onCancel = null)
    {
        TextInputPopup window = GetWindow<TextInputPopup>(true, title);
        window.titleContent = new GUIContent(title);
        window.message = message;
        window.textLabel = textLabel;
        window.onSubmit = onSubmit;
        window.ShowModalUtility();
    }

    private void OnGUI()
    {
        //GUILayout.Label(message, EditorStyles.wordWrappedLabel);
        inputText = EditorGUILayout.TextField(textLabel, inputText);
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            onSubmit?.Invoke(inputText);
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            onCancel?.Invoke();
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}
