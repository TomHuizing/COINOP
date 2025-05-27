using System.Collections;
using InputSystem;
using RainbowArt.CleanFlatUI;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    static public TooltipManager Instance;

    [SerializeField] float delay;
    [SerializeField] RectTransform canvas;
    [SerializeField] TooltipSpecial tooltip;

    private readonly Mouse mouse = new();
    private Coroutine delayCoroutine;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowTooltip(string message)
    {
        if (delayCoroutine != null)
            StopCoroutine(delayCoroutine);
        tooltip.DescriptionValue = message;
        tooltip.UpdatePosition(mouse.ScreenPosition, canvas);

        delayCoroutine = StartCoroutine(ShowWithDelay());
    }

    public void HideTooltip()
    {
        if (delayCoroutine != null)
            StopCoroutine(delayCoroutine);
        tooltip.HideTooltip();
    }

    private IEnumerator ShowWithDelay()
    {
        yield return new WaitForSeconds(delay);
        tooltip.ShowTooltip();
    }
}
