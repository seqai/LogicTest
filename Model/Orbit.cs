namespace LogicTest.Model
{
    internal record Orbit<T>(T OrbitingBody, double OrbitalPeriod, double MeanDistance) : IOrbit<T> where T : ICelestialBody;

    internal interface IOrbit<out T> where T : ICelestialBody

    {
        T OrbitingBody { get; }
        double OrbitalPeriod { get; }
        double MeanDistance { get; }
    }
}
