using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Map;
using Gameplay.Selection;
using UnityEngine;

// using RainbowArt.CleanFlatUI;

namespace Gameplay.Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private float lineWidthMultiplier = 0.01f;
        [SerializeField] private float moveAnimationTime = 0.8f;
        [SerializeField] private LineRenderer movementLineRenderer;

        //[SerializeField] private GameObject selectionUIPrefab;

        private LineRenderer lineRenderer;
        private UnitController controller;

        private Vector2 moveTarget;
        private Coroutine moveCoroutine;
        private float moveAnimationTimeCounter = 0f;

        private readonly List<RegionController> path = new();

        public bool ShowMoveLine
        {
            get => movementLineRenderer != null && movementLineRenderer.enabled;
            set
            {
                if (movementLineRenderer != null)
                    movementLineRenderer.enabled = value;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            controller = GetComponent<UnitController>();
            moveTarget = transform.position;
            // if (TryGetComponent<TooltipHandler>(out var tooltipHandler))
            //     tooltipHandler.Text = name;
            // GenerateMesh();
        }

        // Update is called once per frame
        void Update()
        {
            float lineWidth = Camera.main.orthographicSize * lineWidthMultiplier;
            if (lineRenderer != null)
            {
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
            }
        }

        public void InitSelectionUI(GameObject selectionUI)
        {
            // selectionUI.TryGetComponent<ISelectionUI<UnitController>>(out var selectionUIComponent);
            // if (selectionUIComponent != null)
            //     selectionUIComponent.SelectedObject = controller;
        }

        public void Move(Vector2 targetPosition, bool teleport)
        {
            if (teleport)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                transform.position = targetPosition;
                moveTarget = targetPosition;
                return;
            }
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            transform.position = moveTarget;
            moveTarget = targetPosition;
            moveCoroutine = StartCoroutine(AnimateMove(targetPosition));
        }

        private IEnumerator AnimateMove(Vector2 target)
        {
            moveAnimationTimeCounter = 0f;
            while (moveAnimationTimeCounter < moveAnimationTime)
            {
                moveAnimationTimeCounter += UnityEngine.Time.deltaTime;
                float t = moveAnimationTimeCounter / moveAnimationTime;
                transform.position = Vector2.Lerp(transform.position, target, t);
                UpdateMoveLine();
                yield return null;
            }
            transform.position = target;
            UpdateMoveLine();
        }

        public void PathChange(IEnumerable<RegionController> newPath)
        {
            path.Clear();
            path.AddRange(newPath ?? Enumerable.Empty<RegionController>());
            UpdateMoveLine();
        }

        private void UpdateMoveLine()
        {
            if (movementLineRenderer == null)
                return;
            if (path.Count == 0)
            {
                movementLineRenderer.positionCount = 0;
                return;
            }
            movementLineRenderer.positionCount = path.Count() + 1;
            List<Vector3> positions = new() { transform.position };
            positions.AddRange(path.Select(region => region.transform.position));
            movementLineRenderer.SetPositions(positions.ToArray());
        }
    }
}
