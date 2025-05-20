using TMPro;
using UnityEngine;

public class UnitTooltip : MonoBehaviour
{
    private UnitController controller;

    [SerializeField] private TextMeshProUGUI nameText;

    public UnitController Controller
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
