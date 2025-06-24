using System;
using Gameplay.Map;
using Gameplay.Selection;
using TMPro;
using UnityEngine;

namespace UI.Selection
{
    [Serializable]
    public class RegionSelectionUI : MonoBehaviour, ISelectionUI<RegionController>
    {
        private RegionController regionController;
    
        [SerializeField] private TextMeshProUGUI controlText;
        [SerializeField] private TextMeshProUGUI supportText; 
        [SerializeField] private TextMeshProUGUI infraText;

        public RegionController SelectedObject
        {
            get => regionController;
            set
            {
                regionController = value;
                controlText.text = regionController.Control.ToString("0%");
                supportText.text = regionController.Support.ToString("0%");
                infraText.text = regionController.Infra.ToString("0");
            }
        }

        // Update is called once per frame
        void Update()
        {
            controlText.text = regionController.Control.ToString("0%");
            supportText.text = regionController.Support.ToString("0%");
            infraText.text = regionController.Infra.ToString("0");
        }
    }
}
