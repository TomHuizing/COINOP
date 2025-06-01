using System;

public interface IResourceModifier
{
    public string Name { get; }
    public string Description { get; }
    public int Supplies { get; }
    public int Intel { get; }
    public int Influence { get; }
}
