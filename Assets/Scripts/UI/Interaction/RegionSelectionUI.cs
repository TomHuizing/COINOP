using System;
using Gameplay.Map;
using Gameplay.Selection;
using TMPro;
using UnityEngine;

namespace UI.Interaction
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
                controlText.text = regionController.Stats.Control.ToString("0%");
                supportText.text = regionController.Stats.PopularSupport.ToString("0%");
                infraText.text = regionController.Stats.Infrastructure.ToString("0");
            }
        }

        // Update is called once per frame
        void Update()
        {
            controlText.text = regionController.Stats.Control.ToString("0%");
            supportText.text = regionController.Stats.PopularSupport.ToString("0%");
            infraText.text = regionController.Stats.Infrastructure.ToString("0");
        }
    }
}
