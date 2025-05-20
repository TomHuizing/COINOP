using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MapObject : MonoBehaviour
{
    // public GameManager GameManager;
    // public RegionController StartingRegion;
    // public RegionController DestinationRegion;
    // public float MoveSpeed = 40f;

    // private RegionController currentRegion;
    // private List<RegionController> path = new();

    // private float movePercentage = 0f;

    // public float Speed { get; set; }
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     currentRegion = StartingRegion;
    //     path = FindPath(currentRegion, DestinationRegion);
    //     print($"Path found: {path.Count} regions");
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(path.Count > 0)
    //     {
    //         MoveToNextRegion();
    //     }
    //     else if(transform.position != currentRegion.transform.position)
    //     {
    //         transform.position = currentRegion.transform.position;
    //     }
    // }

    // private void MoveToNextRegion()
    // {
    //     if(path.Count > 0)
    //     {
    //         RegionController nextRegion = path[0];
    //         float totalTime = Vector2.Distance(transform.position, nextRegion.transform.position) / MoveSpeed;
    //         movePercentage += (float)GameManager.DeltaTime.TotalHours / totalTime;
    //         if(movePercentage >= 1f)
    //         {
    //             currentRegion = nextRegion;
    //             path.RemoveAt(0);
    //             movePercentage = 0f;
    //             transform.position = currentRegion.transform.position;
    //         }
    //         else
    //         {
    //             transform.position = Vector2.Lerp(currentRegion.transform.position, nextRegion.transform.position, movePercentage);
    //         }
    //     }
    // }



    // private List<RegionController> ReconstructPath(Dictionary<RegionController, RegionController> cameFrom, RegionController current)
    // {
    //     List<RegionController> path = new();
    //     while (cameFrom.ContainsKey(current))
    //     {
    //         path.Add(current);
    //         current = cameFrom[current];
    //     }
    //     path.Add(current); // Add the start node
    //     path.Reverse(); // Reverse the path to get it from start to end
    //     return path;
    // }

    // private RegionController GetLowestFScore(HashSet<RegionController> openSet, Dictionary<RegionController, float> fScore)
    // {
    //     RegionController lowest = null;
    //     float lowestScore = float.MaxValue;

    //     foreach(RegionController region in openSet)
    //     {
    //         if (fScore.TryGetValue(region, out float score) && score < lowestScore)
    //         {
    //             lowest = region;
    //             lowestScore = score;
    //         }
    //     }

    //     return lowest;
    // }

    // private List<RegionController> FindPath(RegionController start, RegionController end)
    // {
    //     HashSet<RegionController> openSet = new();
    //     HashSet<RegionController> closedSet = new();
    //     Dictionary<RegionController, RegionController> cameFrom = new();
    //     Dictionary<RegionController, float> gScore = new();
    //     Dictionary<RegionController, float> fScore = new();

    //     openSet.Add(start);
    //     gScore[start] = 0;
    //     fScore[start] = Vector2.Distance(start.transform.position, end.transform.position);

    //     while (openSet.Count > 0)
    //     {
    //         RegionController current = GetLowestFScore(openSet, fScore);
    //         if (current == end)
    //             return ReconstructPath(cameFrom, current);

    //         openSet.Remove(current);
    //         closedSet.Add(current);

    //         foreach (RegionController neighbor in current.Neighbors)
    //         {
    //             if (closedSet.Contains(neighbor))
    //                 continue;

    //             float tentativeGScore = gScore[current] + Vector2.Distance(current.transform.position, neighbor.transform.position);
    //             if (!openSet.Contains(neighbor))
    //                 openSet.Add(neighbor);
    //             else if (tentativeGScore >= gScore[neighbor])
    //                 continue;

    //             cameFrom[neighbor] = current;
    //             gScore[neighbor] = tentativeGScore;
    //             fScore[neighbor] = gScore[neighbor] + Vector2.Distance(neighbor.transform.position, end.transform.position);
    //         }
    //     }

    //     return new(); // No path found
    // }
}
