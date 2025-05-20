using UnityEngine;

public class UnitModel
{
    public string Name { get; private set; }
    public float Strength { get; private set; }
    public float Supplies { get; private set; }

    public UnitModel(string name)
    {
        Name = name;
        Strength = Random.Range(0.5f, 1.5f);
        Supplies = Random.Range(0.5f, 1.5f);
    }
}
