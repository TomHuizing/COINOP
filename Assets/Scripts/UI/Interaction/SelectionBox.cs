using UnityEngine;

namespace UI.Interaction
{
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
            if(lineRenderer.enabled)
            {
                lineRenderer.startWidth = lineWidth * Camera.main.orthographicSize;
                lineRenderer.endWidth = lineRenderer.startWidth;
            }
        }

        public void SetBox(Rect rect)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, new(rect.xMin, rect.yMax, 0));
            lineRenderer.SetPosition(1, new(rect.xMax, rect.yMax, 0));
            lineRenderer.SetPosition(2, new(rect.xMax, rect.yMin, 0));
            lineRenderer.SetPosition(3, new(rect.xMin, rect.yMin, 0));
        }

        public void Hide()
        {
            lineRenderer.enabled = false;
        }


    }
}
