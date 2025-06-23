// using System.Collections;
// using RainbowArt.CleanFlatUI;
// using UnityEngine;

// [RequireComponent(typeof(Canvas))]
// public class TooltipCanvas : MonoBehaviour
// {
//     public static TooltipCanvas Instance { get; private set; }

//     private RectTransform tooltip;

//     [SerializeField] private Vector2 tooltipOffset = new(10, -10);
//     [SerializeField] private float tooltipDelay = 0.5f;
//     [SerializeField] private TooltipSpecial tooltipSpecial;

//     Coroutine tooltipDelayCoroutine;


//     public RectTransform Tooltip
//     {
//         get => tooltip;
//         set
//         {
//             if (tooltipDelayCoroutine != null)
//             {
//                 StopCoroutine(tooltipDelayCoroutine);
//                 tooltipDelayCoroutine = null;
//             }
//             if (tooltip != null)
//             {
//                 Destroy(tooltip.gameObject);
//             }
//             tooltip = value;
//             if (tooltip != null)
//             {
//                 tooltip.SetParent(transform);
//                 tooltip.localScale = Vector3.one;
//                 tooltip.localPosition = Vector3.zero;
//                 tooltip.gameObject.SetActive(false);
//                 tooltipDelayCoroutine = StartCoroutine(ShowTooltipAfterDelay(tooltip, tooltipDelay));
//             }
//         }
//     }

//     void Awake()
//     {
//         if (Instance == null)
//             Instance = this;
//         else
//             Destroy(gameObject);
//     }


//     void Update()
//     {
//         if (tooltip != null && tooltip.gameObject.activeSelf)
//         {
//             PositionTooltip();
//         }
//     }

//     public IEnumerator ShowTooltipAfterDelay(RectTransform tooltip, float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         if (tooltip != null)
//         {
//             PositionTooltip();
//             tooltip.gameObject.SetActive(true);
//         }
//     }

//     private void PositionTooltip()
//     {
//         Vector2 size = tooltip.rect.size;
//         Vector2 offset = tooltipOffset;
//         if (Input.mousePosition.x + size.x + offset.x > Screen.width)
//         {
//             tooltip.pivot = new(1, tooltip.pivot.y);
//             offset.x = -offset.x;
//         }
//         else
//         {
//             tooltip.pivot = new(0, tooltip.pivot.y);
//         }

//         if (Input.mousePosition.y + size.y + offset.y > Screen.height)
//         {
//             tooltip.pivot = new(tooltip.pivot.x, 1);
//             offset.x = -offset.x;
//         }
//         else
//         {
//             tooltip.pivot = new(tooltip.pivot.x, 0);
//         }

//         tooltip.position = Input.mousePosition + (Vector3)offset;
//     }

//     public void ShowTooltip(string text)
//     {
//         if (tooltipSpecial == null)
//             return;
//         tooltipSpecial.DescriptionValue = text;
//         tooltipSpecial.UpdatePosition(Input.mousePosition, GetComponent<RectTransform>());
//         tooltipSpecial.ShowTooltip();
//     }
// }
