using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D), typeof(RegionController))]
[ExecuteInEditMode]
public class RegionView : MonoBehaviour
{
    [SerializeField] private float lineWidthMultiplier = 0.01f;

    //[SerializeField] private GameObject selectionUIPrefab;

    private LineRenderer lineRenderer;
    private RegionController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        controller = GetComponent<RegionController>();
        // GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        float lineWidth = Camera.main.orthographicSize * lineWidthMultiplier;
        if(lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }
    }

    public void InitSelectionUI(GameObject selectionUI) => selectionUI.GetComponent<RegionSelectionUI>().RegionController = GetComponent<RegionController>();

    [UnityEngine.ContextMenu("Generate Mesh")]
    public void GenerateMesh()
    {
        var filter = GetComponent<MeshFilter>();
        var poly = GetComponent<PolygonCollider2D>();

        Vector2[] points = poly.points;
        Vector3[] vertices = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            vertices[i] = points[i];

        Triangulator triangulator = new(points);
        int[] indices = triangulator.Triangulate();

        Mesh mesh = new()
        {
            vertices = vertices,
            triangles = indices
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        filter.sharedMesh = mesh;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(vertices);
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    public void TooltipShow(GameObject tooltip)
    {
        if (tooltip.TryGetComponent(out RegionTooltip regionTooltip))
        {
            regionTooltip.Controller = controller;
        }
    }
}
