using Gameplay.Selection;
using Gameplay.Units;
using TMPro;
using UnityEngine;

namespace UI.Interaction
{
    public class UnitSelectionUi : MonoBehaviour, ISelectionUI<UnitController>
    {
        private UnitController unitController;

        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI suppliesText;

        public UnitController SelectedObject
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
            if(unitController == null)
                return;
            targetText.text = unitController.TargetRegion != null ? unitController.NextRegion.Name + " | " + unitController.TargetRegion.Name : "";
            strengthText.text = unitController.Strength.ToString("0%");
            suppliesText.text = unitController.Supplies.ToString("0%");
        }
    }
}
