using UnityEngine;

[ExecuteInEditMode]
public class MapTransform : MonoBehaviour
{
    [SerializeField] private OsmImporter osmImporter;
    [SerializeField] private Vector2 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
            return;
        transform.position = LatLongToWorldPos(position);
#endif
    }
    
    private Vector2 LatLongToWorldPos(Vector2 latLong)
    {
        const double earthRadius = 6378137; // in meters
        double dLat = (latLong.x - osmImporter.CenterLatLon.x) * Mathf.Deg2Rad;
        double dLon = (latLong.y - osmImporter.CenterLatLon.y) * Mathf.Deg2Rad;
        double x = earthRadius * dLon * Mathf.Cos((float)(osmImporter.CenterLatLon.x * Mathf.Deg2Rad));
        double y = earthRadius * dLat;

        return new Vector2((float)x / osmImporter.MapScale, (float)y / osmImporter.MapScale);
    }
}
