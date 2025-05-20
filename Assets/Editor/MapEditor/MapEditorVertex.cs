using UnityEngine;
using UnityEditor;

public class MapEditorVertex
{
    private static int idCounter = 0;
    public int Id { get; private set; }
    public Vector2 Position;

    public MapEditorVertex(Vector2 position)
    {
        this.Position = position;
        this.Id = idCounter++;
    }

    public void Draw(bool isSelected)
    {
        float handleSize = HandleUtility.GetHandleSize(Position) * 0.05f;
        Color prevColor = Handles.color;

        Handles.color = isSelected ? Color.yellow : Color.white;
        Handles.DotHandleCap(0, Position, Quaternion.identity, handleSize, EventType.Repaint);

        Handles.color = prevColor;
    }
}
