using System;
using UnityEngine;

[Serializable]
public struct SerializableDateTime
{
    [SerializeField] private long ticks;

    public DateTime Value
    {
        get => new(ticks);
        set => ticks = value.Ticks;
    }

    public SerializableDateTime(DateTime dateTime)
    {
        ticks = dateTime.Ticks;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}