using UnityEngine;

public readonly struct GeoCoord
{
    public readonly float Latitude;
    public readonly float Longtitude;

    private const float EarthRadius = 6371e3f; // Radius of the Earth in meters

    public GeoCoord(float latitude, float longtitude)
    {
        Latitude = latitude;
        Longtitude = longtitude;
    }

    public float DistanceTo(GeoCoord other)
    {
        // Haversine formula to calculate distance between two points on the Earth
        float lat1 = Latitude * Mathf.Deg2Rad;
        float lat2 = other.Latitude * Mathf.Deg2Rad;
        float deltaLat = (other.Latitude - Latitude) * Mathf.Deg2Rad;
        float deltaLon = (other.Longtitude - Longtitude) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
                  Mathf.Cos(lat1) * Mathf.Cos(lat2) *
                  Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return EarthRadius * c; // Distance in meters
    }
}
