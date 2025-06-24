using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSegment
{
    public readonly long[] Nodes;
    public readonly string Name;

    public RoadSegment(string name, long[] nodes)
    {
        Name = name;
        Nodes = nodes;
    }
}
