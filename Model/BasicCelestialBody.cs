namespace LogicTest.Model
{
    internal class BasicCelestialBody : ICelestialBody
    {
        protected static Random Rng = new(Environment.TickCount);
        public BasicCelestialBody(string name, double mass, string picture = "")
        {
            Name = name;
            Mass = mass;

            if (string.IsNullOrWhiteSpace(picture))
            {
                Picture = $"{Uri.EscapeDataString(name.ToLowerInvariant())}.jpg";
            } 
        }

        public string Name { get; }
        public double Mass { get; }
        public string Picture { get; }
    }
}
