using UnityEngine;

public class PeriodicResourceChanger : MonoBehaviour, IPeriodicResourceChanger
{
    [SerializeField] private string description;
    [SerializeField] private int supplies;
    [SerializeField] private int intel;
    [SerializeField] private int influence;


    public string Name => name;
    public string Description => description;
    public int Supplies => supplies;
    public int Intel => intel;
    public int Influence => influence;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ResourceManager.Instance.RegisterPeriodicResourceChanger(this);
    }
}
