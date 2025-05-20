using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class TooltipCanvas : MonoBehaviour
{
    public static TooltipCanvas Instance { get; private set; }

    private GameObject tooltip;

    [SerializeField] private Vector2 tooltipOffset = new Vector2(10, -10);
    [SerializeField] private float tooltipDelay = 0.5f;

    private float tooltipTimer;

    public GameObject Tooltip
    {
        get => tooltip;
        set
        {
            if(tooltip != null)
            {
                Destroy(tooltip);
            }
            tooltip = value;
            if (tooltip != null)
            {
                tooltip.transform.SetParent(transform);
                tooltip.transform.localScale = Vector3.one;
                tooltip.transform.localPosition = Vector3.zero;
                tooltip.SetActive(false);
            }
            else
            {
                tooltipTimer = tooltipDelay;
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        tooltipTimer = tooltipDelay;
    }

    void Update()
    {
        if(tooltip != null && tooltipTimer > 0)
        {
            tooltipTimer -= Time.deltaTime;
            if (tooltipTimer <= 0)
            {
                tooltip.SetActive(true);
                tooltip.transform.position = Input.mousePosition + (Vector3)tooltipOffset;
            }
        }
        else if(tooltip != null && tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition + (Vector3)tooltipOffset;
        }
    }
}
