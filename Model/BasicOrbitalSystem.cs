namespace LogicTest.Model
{
    internal class BasicOrbitalSystem<T> : BasicCelestialBody, IOrbitalSystem<T> where T : ICelestialBody
    {
        private readonly List<IOrbit<T>> _satellites = new();

        public BasicOrbitalSystem(string name, double mass, IEnumerable<IOrbit<T>> satellites, string picture = "") : base(name, mass, picture)
        {
            _satellites.AddRange(satellites);
        }

        public IReadOnlyCollection<IOrbit<T>> Satellites => _satellites.AsReadOnly();
    }
}
