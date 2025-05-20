using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using System.Collections.Generic;

[EditorTool("Map Editor Tool")]
public class MapEditorTool : EditorTool
{
    private MapData mapData;
    private int selectedVertex = -1;
    private const float HandleSize = 0.1f;

    public override void OnToolGUI(EditorWindow window)
    {
        if (!(Tools.current == Tool.None)) return;

        Event e = Event.current;
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        if (Selection.activeGameObject)
            mapData = Selection.activeGameObject.GetComponent<MapData>();

        if (!mapData)
            return;

        // Draw all vertices
        for (int i = 0; i < mapData.Vertices.Count; i++)
        {
            Vector2 worldPos = mapData.Vertices[i];
            Vector3 handlePos = new Vector3(worldPos.x, worldPos.y, 0f);
            float size = HandleUtility.GetHandleSize(handlePos) * HandleSize;

            Handles.color = (i == selectedVertex) ? Color.yellow : Color.white;
            if (Handles.Button(handlePos, Quaternion.identity, size, size, Handles.DotHandleCap))
            {
                selectedVertex = i;
                e.Use();
            }

            if (i == selectedVertex && e.type == EventType.MouseDrag && e.button == 0)
            {
                Undo.RecordObject(mapData, "Move Vertex");
                Vector3 newPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
                mapData.Vertices[i] = new Vector2(newPos.x, newPos.y);
                e.Use();
            }
        }

        // Add vertex on Ctrl+click
        if (e.type == EventType.MouseDown && e.button == 0 && e.control)
        {
            Undo.RecordObject(mapData, "Add Vertex");
            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            mapData.Vertices.Add(new Vector2(mousePos.x, mousePos.y));
            e.Use();
        }

        // Draw regions
        foreach (var region in mapData.Regions)
        {
            if (region.Count < 2) continue;
            for (int i = 0; i < region.Count; i++)
            {
                Vector2 from = mapData.Vertices[region[i]];
                Vector2 to = mapData.Vertices[region[(i + 1) % region.Count]];
                Handles.color = Color.green;
                Handles.DrawLine(new Vector3(from.x, from.y, 0), new Vector3(to.x, to.y, 0));
            }
        }

        if (GUI.changed)
            EditorUtility.SetDirty(mapData);
    }
}
