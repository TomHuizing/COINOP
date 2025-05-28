using TMPro;
using UnityEngine;

public class UnitSelectionUi : MonoBehaviour
{
    private UnitController unitController;

    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI suppliesText;

    public UnitController UnitController
    {
        get => unitController;
        set
        {
            unitController = value;
            targetText.text = unitController.TargetRegion != null ? unitController.NextRegion.Name + " | " + unitController.TargetRegion.Name : "";
            strengthText.text = unitController.Strength.ToString("0%");
            suppliesText.text = unitController.Supplies.ToString("0%");
        }
    }

    void Update()
    {
        targetText.text = unitController.TargetRegion != null ? unitController.NextRegion.Name + " | " + unitController.TargetRegion.Name : "";
        strengthText.text = unitController.Strength.ToString("0%");
        suppliesText.text = unitController.Supplies.ToString("0%");
    }
}
