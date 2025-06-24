using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Map;
using Gameplay.Time;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Components
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private GameClock clock;
        [SerializeField] private RegionController currentRegion; // Reference to the RegionController this unit belongs to
        [SerializeField] private float moveSpeed = 1f; // Speed of the unit's movement
        [SerializeField] private UnityEvent<Vector2, bool> onMove = new();
        [SerializeField] private UnityEvent<IEnumerable<RegionController>> onPathChange = new();

        private readonly List<RegionController> path = new();
        private float distLeft = 0f; // Distance left to the target region

        public RegionController CurrentRegion
        {
            get => currentRegion;
            private set
            {
                currentRegion = value; // Set the current region for the unit
                onMove.Invoke(currentRegion.transform.position, false); // Reset the unit's position to the current region's position
            }
        }

        public IEnumerable<RegionController> Path => path; // Read-only property to access the path
        public float MoveSpeed { get => moveSpeed; set { moveSpeed = value; } } // Read-only property to access the move speed

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ResetPosition(); // Reset the unit's position to the current region
        }

        void OnEnable()
        {
            clock.OnTick += Tick;
        }

        void OnDisable()
        {
            clock.OnTick -= Tick;
        }

        public void Tick(DateTime now, TimeSpan period)
        {
            if (path == null || path.Count == 0)
                return; // Check if the path is valid
            
            distLeft -= moveSpeed * 10f * (float)period.TotalHours; // Decrease the distance left to the target region based on move speed and time

            if (distLeft <= 0f)
            {
                distLeft = 0f; // Reset distance left to zero if it goes negative
                CurrentRegion = path.First(); // Update the current region to the next region in the path
                path.Remove(CurrentRegion); // Remove the reached region from the path
                if (path.Count > 0)
                    distLeft = Vector2.Distance(currentRegion.transform.position, path[0].transform.position); // Calculate the distance to the target region
                onPathChange.Invoke(path); // Invoke the path change event with the new path
                return;
            }

            float pctLeft = distLeft / Vector2.Distance(currentRegion.transform.position, path[0].transform.position); // Calculate the percentage of distance left to the target region

            onMove.Invoke(Vector2.Lerp(currentRegion.transform.position, path[0].transform.position, 1 - pctLeft), false); // Invoke the move event with the current region's position
        }

        private void ResetPosition()
        {
            distLeft = 0f; // Reset distance left to zero
            if (currentRegion == null)
                return; // Check if the current region is valid
            onMove.Invoke(currentRegion.transform.position, true); // Reset the unit's position to the current region's position
        }

        public void SetTarget(RegionController target)
        {
            if (target == null)
                return; // Check if the target region is valid
            ResetPosition();
            path.Clear(); // Clear the path list before setting a new target
            if (target == currentRegion)
                return; // Check if the target region is the same as the current region
            FindPath(currentRegion, target); // Get the path to the target region
            if (path.Count > 0)
                distLeft = Vector2.Distance(currentRegion.transform.position, path.First().transform.position); // Calculate the distance to the target region
            onPathChange.Invoke(path); // Invoke the path change event with the new path
        }

        private void FindPath(RegionController start, RegionController goal)
        {
            var openSet = new List<RegionController>();
            var cameFrom = new Dictionary<RegionController, RegionController>();
            var gScore = new Dictionary<RegionController, float>();
            var fScore = new Dictionary<RegionController, float>();

            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            openSet.Add(start); // Add the start region to the open set

            while (openSet.Count > 0)
            {
                var current = openSet.First(); // Get the region with the lowest fScore from the open set
                openSet.Remove(current); // Remove the current region from the open set

                if (current == goal)
                {
                    ApplyPath(cameFrom, current);
                    return;
                }

                foreach (var neighbor in current.Neighbors)
                {
                    float tentativeGScore = gScore[current] + Vector2.Distance(current.transform.position, neighbor.transform.position); // Calculate the tentative gScore for the neighbor

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                        {
                            int i = openSet.FindLastIndex(x => fScore[x] < fScore[neighbor]); // Add the start region to the open set
                            openSet.Insert(i + 1, neighbor); // Insert the neighbor into the open set based on its fScore
                        }
                    }
                }
            }
        }

        private float Heuristic(RegionController a, RegionController b)
        {
            return Vector2.Distance(a.transform.position, b.transform.position);
        }

        private void ApplyPath(Dictionary<RegionController, RegionController> cameFrom, RegionController goal)
        {
            path.Clear(); // Clear the path list before reconstructing it
            path.Add(goal); // Add the current region to the path

            RegionController current = goal; // Set the current region to the goal region

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            while (path.FirstOrDefault() == currentRegion)
                path.RemoveAt(0); // Remove the current region from the path if it is the first region
            if (path.Count > 0)
                distLeft = Vector2.Distance(currentRegion.transform.position, path.First().transform.position); // Calculate the distance to the target region
        }

        public void Cycle(CyclePeriod period, DateTime dateTime) { }
        public void Day(DateTime dateTime) { }
        public void TimeStart() { }
        public void TimeStop() { }
    }
}
