using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class OsmImporter : MonoBehaviour
{
    [Header("Osm file")]
    public TextAsset osmFile;

    [Header("Map settings")]
    // public double centerLatitude;
    // public double centerLongitude;
    [SerializeField] private Vector2 centerLatLon;
    [Tooltip("In meters per unit")]
    [SerializeField] private float mapScale = 100f;

    [Header("Prefabs")]
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject residentialPrefab;
    [SerializeField] private GameObject fieldPrefab;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject waterWayPrefab;
    [SerializeField] private GameObject regionPrefab;

    private readonly Dictionary<long, Vector2> nodes = new();
    private readonly Dictionary<long, List<Vector2>> ways = new();
    private readonly Dictionary<long, List<long>> relations = new();

    public Vector2 CenterLatLon => centerLatLon;
    public float MapScale => mapScale;

    [ContextMenu("Import OSM")]
    public void ImportOsm()
    {
        
        if (osmFile == null)
        {
            Debug.LogError("No OSM file selected.");
            return;
        }

        List<MapFeatureData> features = new();
        List<List<Vector2>> roads = new();
        XDocument osmDocument = XDocument.Parse(osmFile.text);

        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }


        // Parse nodes
        foreach (XElement node in osmDocument.Descendants("node"))
        {
            long id = (long)node.Attribute("id");
            double lat = (double)node.Attribute("lat");
            double lon = (double)node.Attribute("lon");

            Vector2 position = LatLonToPosition(lat, lon);
            nodes[id] = position;
        }

        // Parse ways
        foreach (XElement way in osmDocument.Descendants("way"))
        {
            long id = (long)way.Attribute("id");
            List<Vector2> wayNodes = new();
            foreach (var nd in way.Descendants("nd"))
            {
                long refId = (long)nd.Attribute("ref");
                if (nodes.TryGetValue(refId, out Vector2 position))
                {
                    wayNodes.Add(position);
                }
            }

            if (wayNodes.Count > 0)
                ways[id] = wayNodes;

            Dictionary<string, string> tags = way.Elements("tag").ToDictionary(
                tag => tag.Attribute("k")?.Value ?? "",
                tag => tag.Attribute("v")?.Value ?? "");

            FeatureType featureType = Classify(tags);
            if(featureType == FeatureType.Road)
            {
                string name = tags.ContainsKey("name") ? tags["name"] : "Unnamed Road";
                List<Vector2> appendRoad = roads.FirstOrDefault(r => r.Last() == wayNodes.First());
                List<Vector2> prependRoad = roads.FirstOrDefault(r => r.First() == wayNodes.Last());

                if(appendRoad != null && prependRoad != null)
                {
                    appendRoad.AddRange(wayNodes.Skip(1));
                    appendRoad.AddRange(prependRoad.Skip(1));
                    roads.Remove(prependRoad);
                }
                else if(appendRoad != null)
                {
                    appendRoad.AddRange(wayNodes.Skip(1));
                }
                else if(prependRoad != null)
                {
                    prependRoad.InsertRange(0, wayNodes.SkipLast(1));
                }
                else
                {
                    roads.Add(wayNodes);
                }
            }
            else if(featureType == FeatureType.Water)
            {
                GameObject waterObject = Instantiate(waterPrefab, transform);
                waterObject.GetComponent<MapPolygon>().SetVertices(wayNodes);
                waterObject.name = "Water";
            }
            else if(featureType == FeatureType.WaterWay)
            {
                // GameObject waterWayObject = Instantiate(waterWayPrefab, transform);
                // waterWayObject.GetComponent<MapLine>().SetVertices(wayNodes);
                // waterWayObject.name = "WaterWay";
            }
            else if(featureType == FeatureType.Residential)
            {
                GameObject residentialObject = Instantiate(residentialPrefab, transform);
                residentialObject.GetComponent<MapPolygon>().SetVertices(wayNodes);
                residentialObject.name = "Residential";
            }
            else if(featureType == FeatureType.Uncultivated)
            {
                GameObject fieldObject = Instantiate(fieldPrefab, transform);
                fieldObject.GetComponent<MapPolygon>().SetVertices(wayNodes);
                fieldObject.name = "Field";
            }
            else if(featureType == FeatureType.Other)
            {
                // Handle other types of features if needed
            }
            // Check for tags to determine the type of way
        }



        foreach(List<Vector2> r in roads)
        {
            GameObject roadObject = Instantiate(roadPrefab, transform);
            roadObject.GetComponent<MapLine>().SetVertices(r);
            roadObject.GetComponent<MapLine>().Width = 0.02f;
            roadObject.name = "Unnamed road";
        }

        // Parse relations
        // foreach (var relation in osmDocument.Descendants("relation"))
        // {
        //     List<Vector2> relationNodes = new();
        //     foreach (var member in relation.Descendants("member"))
        //     {
        //         string type = (string)member.Attribute("type");
        //         long refId = (long)member.Attribute("ref");

        //         if (type == "way" && nodes.TryGetValue(refId, out Vector2 position))
        //         {
        //             relationNodes.Add(position);
        //         }
        //     }

        //     if (relationNodes.Count > 0)
        //     {
        //         CreateRegion(relationNodes);
        //     }
        // }
    }

    private Vector2 LatLonToPosition(double lat, double lon)
    {
        // Convert latitude and longitude to Unity world position
        const double earthRadius = 6378137; // in meters
        double dLat = (lat - centerLatLon.x) * Mathf.Deg2Rad;
        double dLon = (lon - centerLatLon.y) * Mathf.Deg2Rad;
        double x = earthRadius * dLon * Mathf.Cos((float)(centerLatLon.x * Mathf.Deg2Rad));
        double y = earthRadius * dLat;

        return new Vector2((float)x / mapScale, (float)y / mapScale);
    }

    private void ClearChildren()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private FeatureType Classify(Dictionary<string, string> tags)
    {
        if (tags.TryGetValue("landuse", out string landuse))
        {
            if (landuse == "residential" || landuse == "commercial") return FeatureType.Residential;
            if (landuse == "grass" || landuse == "meadow" || landuse == "military") return FeatureType.Uncultivated;
        }

        if (tags.TryGetValue("place", out string place))
        {
            if (place == "neighbourhood" || place == "suburb") return FeatureType.Residential;
        }

        if (tags.TryGetValue("natural", out string natural))
        {
            if (natural == "water") return FeatureType.Water;
            else return FeatureType.Uncultivated;
            //if (natural == "scrub" || natural == "heath") return FeatureType.Uncultivated;
        }

        if (tags.ContainsKey("water")) return FeatureType.Water;
        if (tags.ContainsKey("waterway")) return FeatureType.WaterWay;

        // if (tags.TryGetValue("waterway", out string waterway))
        // {
        //     if (waterway == "riverbank" |) return FeatureType.Water;
        // }

        if (tags.ContainsKey("highway")) return FeatureType.Road;

        return FeatureType.Other;
    }
}
