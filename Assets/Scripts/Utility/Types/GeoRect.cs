public readonly struct GeoRect
{
    public readonly GeoCoord NorthWest;
    public readonly GeoCoord SouthEast;

    public readonly float NorthernWidth;
    public readonly float SouthernWidth;
    public readonly float Height;
    public readonly GeoCoord Center;
    public readonly float CenterWidth;

    public GeoRect(GeoCoord northWest, GeoCoord southEast)
    {
        NorthWest = northWest;
        SouthEast = southEast;

        GeoCoord northEast = new(NorthWest.Latitude, SouthEast.Longtitude);
        GeoCoord southWest = new(SouthEast.Latitude, NorthWest.Longtitude);
        NorthernWidth = northEast.DistanceTo(NorthWest);
        SouthernWidth = southWest.DistanceTo(SouthEast);
        Height = NorthWest.DistanceTo(southWest);
        Center = new GeoCoord((NorthWest.Latitude + SouthEast.Latitude) / 2, (NorthWest.Longtitude + SouthEast.Longtitude) / 2);
        CenterWidth = new GeoCoord(Center.Latitude, NorthWest.Latitude).DistanceTo(new GeoCoord(Center.Latitude, SouthEast.Latitude));
    }

    public GeoRect(GeoCoord center, float centerWidth, float height)
    {
        Center = center;
        CenterWidth = centerWidth;
        Height = height;

        float halfHeight = height / 2;
        float halfWidth = centerWidth / 2;

        NorthWest = new GeoCoord(center.Latitude + halfHeight, center.Longtitude - halfWidth);
        SouthEast = new GeoCoord(center.Latitude - halfHeight, center.Longtitude + halfWidth);

        NorthernWidth = new GeoCoord(NorthWest.Latitude, SouthEast.Longtitude).DistanceTo(NorthWest);
        SouthernWidth = new GeoCoord(SouthEast.Latitude, NorthWest.Longtitude).DistanceTo(SouthEast);
    }
}
