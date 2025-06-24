using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UiController", menuName = "Ui/UiController")]
public class UiController : ScriptableObject
{
    public void ShowTooltip(string message)
    {
        // Implement tooltip display logic here
        Debug.Log($"Tooltip: {message}");
    }

    public void ShowContextMenu(string[] options, Action<int> callback)
    {
        // Implement context menu display logic here
        Debug.Log("Context menu displayed");
        UI.Elements.ContextMenu.Instance.Show(Input.mousePosition, options, callback);
    }
}
