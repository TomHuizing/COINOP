using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        lineRenderer.positionCount = 4;
        lineRenderer.loop = true;
    }

    void Update()
    {
        lineRenderer.startWidth = lineWidth * Camera.main.orthographicSize;
        lineRenderer.endWidth = lineRenderer.startWidth;
    }

    public void SetBox(Vector2 topLeft, Vector2 botRight)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, new(topLeft.x, topLeft.y, 0));
        lineRenderer.SetPosition(1, new(botRight.x, topLeft.y, 0));
        lineRenderer.SetPosition(2, new(botRight.x, botRight.y, 0));
        lineRenderer.SetPosition(3, new(topLeft.x, botRight.y, 0));
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }


}
