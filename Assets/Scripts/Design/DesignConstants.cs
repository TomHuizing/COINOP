using UnityEngine;

public class DesignConstants : MonoBehaviour
{
    public static DesignConstants Instance { get; private set; }

    [Header("Text Colors")]
    [SerializeField] Color textColor;
    [SerializeField] Color highlightColor;
    [SerializeField] Color subduedColor;
    [SerializeField] Color warningColor;
    [SerializeField] Color dangerColor;

    [Header("Panel Colors")]
    [SerializeField] Color backgroundColor;
    [SerializeField] Color backgroundAltColor;
    [SerializeField] Color borderColor;

    [Header("Button Colors")]
    [SerializeField] Color buttonColor;
    [SerializeField] Color buttonHoverColor;
    [SerializeField] Color buttonPressedColor;
    [SerializeField] Color buttonDisabledColor;

    [Header("Common Colors")]
    [SerializeField] Color red = new(0.545f, 0.227f, 0.227f);
    [SerializeField] Color blue = new(0.29f, 0.416f, 0.541f);
    [SerializeField] Color green = new(0.42f, 0.557f, 0.137f);
    [SerializeField] Color yellow = new(0.761f, 0.631f, 0.302f);

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
