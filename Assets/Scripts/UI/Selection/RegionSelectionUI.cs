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
                controlText.text = $"<link=\"{regionController.Id}\">{regionController.Stats.Control:0%}</link>";
                supportText.text = regionController.Stats.PopularSupport.ToString("0%");
                infraText.text = regionController.Stats.Infrastructure.ToString("0");
            }
        }

        // Update is called once per frame
        void Update()
        {
            controlText.text = $"<link=\"{regionController.Id}\">{regionController.Stats.Control:0%}</link>";
            supportText.text = regionController.Stats.PopularSupport.ToString("0%");
            infraText.text = regionController.Stats.Infrastructure.ToString("0");
        }
    }
}
