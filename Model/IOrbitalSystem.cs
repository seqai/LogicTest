namespace LogicTest.Model
{
    internal interface IOrbitalSystem<out T> : ICelestialBody where T : ICelestialBody
    {
        IReadOnlyCollection<IOrbit<T>> Satellites { get; }
    }
}
