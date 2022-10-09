namespace AirportApiTest.Services;

public static class DistanceService
{
    const double D2R = Math.PI / 180.0;

    public static dynamic CalculateTwoPointDistance(Point a, Point b)
        //where T : INumber<T>
    {
        var dXSquare = Math.Abs(Math.Pow(b.X - a.X, 2));
        var dYSquare = Math.Abs(Math.Pow(b.Y - a.Y, 2));

        return Math.Sqrt(dXSquare + dYSquare);
    }

    public static double CalculateTwoPointGeoDistanceInMile(Point pos1, Point pos2, DistanceType type)
    {
        double R = (type == DistanceType.Miles) ? 3960 : 6371;

        double dLat = (pos2.Y - pos1.Y) * D2R;
        double dLon = (pos2.X - pos1.X) * D2R;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(pos1.Y * D2R) * Math.Cos(pos2.Y * D2R) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        double d = R * c;

        return d;
    }

    public enum DistanceType { Miles, Kilometers };
}

public struct Point
{
    public dynamic X;
    public dynamic Y;
}