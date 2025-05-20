using TMPro;
using UnityEngine;

public class RegionTooltip : MonoBehaviour
{
    private RegionController controller;

    [SerializeField] private TextMeshProUGUI nameText;

    public RegionController Controller
    {
        get => controller;
        set
        {
            controller = value;
            nameText.text = controller != null ? controller.Name : "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = controller != null ? controller.Name : "";
    }
}
