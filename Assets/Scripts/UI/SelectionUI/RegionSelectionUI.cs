using System;
using TMPro;
using UnityEngine;

[Serializable]
public class RegionSelectionUI : MonoBehaviour
{
    private RegionController regionController;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI controlText;
    [SerializeField] private TextMeshProUGUI supportText; 
    [SerializeField] private TextMeshProUGUI infraText;

    public RegionController RegionController
    {
        get => regionController;
        set
        {
            regionController = value;
            nameText.text = regionController.Name;
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
