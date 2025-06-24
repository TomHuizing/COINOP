using UnityEngine;

[CreateAssetMenu(fileName = "UiController", menuName = "Ui/UiController")]
public class UiController : ScriptableObject
{
    public void ShowTooltip(string message)
    {
        // Implement tooltip display logic here
        Debug.Log($"Tooltip: {message}");
    }
}
