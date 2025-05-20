using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI suppliesText;
    [SerializeField] private TextMeshProUGUI intelText;
    [SerializeField] private TextMeshProUGUI influenceText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResourcesChanged(int supplies, int intel, int influence)
    {
        suppliesText.text = supplies.ToString();
        intelText.text = intel.ToString();
        influenceText.text = influence.ToString();
    }
}
