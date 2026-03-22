namespace BuildingBlocks.Domain.ValueObjects;

public sealed record GeoPoint(decimal Latitude, decimal Longitude)
{
    public static GeoPoint Create(decimal latitude, decimal longitude)
    {
        if (latitude < -90 || latitude > 90) throw new ArgumentOutOfRangeException(nameof(latitude));
        if (longitude < -180 || longitude > 180) throw new ArgumentOutOfRangeException(nameof(longitude));
        return new GeoPoint(latitude, longitude);
    }
}
