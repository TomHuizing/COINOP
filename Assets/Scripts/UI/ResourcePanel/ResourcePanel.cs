using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private TextMeshProUGUI suppliesText;
    [SerializeField] private TextMeshProUGUI intelText;
    [SerializeField] private TextMeshProUGUI influenceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (resourceManager != null)
        {
            suppliesText.text = resourceManager.Supplies.ToString();
            intelText.text = resourceManager.Intel.ToString();
            influenceText.text = resourceManager.Influence.ToString();
            resourceManager.OnResourceChanged += UpdateText;
        }
    }

    private void UpdateText(int supplies, int intel, int influence)
    {
        suppliesText.text = supplies.ToString();
        intelText.text = intel.ToString();
        influenceText.text = influence.ToString();
    }

    void Update()
    {
        suppliesText.text = ResourceManager.instance.Supplies.ToString();
        intelText.text = ResourceManager.instance.Intel.ToString();
        influenceText.text = ResourceManager.instance.Influence.ToString();
    }
}
