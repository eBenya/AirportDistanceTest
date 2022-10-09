namespace AirportApiTest.Services;

public static class DistanceService
{
    public static dynamic Calculate(Point a, Point b)
        //where T : INumber<T>
    {
        var dXSquare = Math.Abs(Math.Pow(b.X - a.X, 2));
        var dYSquare = Math.Abs(Math.Pow(b.Y - a.Y, 2));

        return Math.Sqrt(dXSquare + dYSquare);
    }
}

public struct Point
{
    public dynamic X;
    public dynamic Y;
}