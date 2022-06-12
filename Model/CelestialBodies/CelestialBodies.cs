using LogicTest.Model.Colonization;

namespace LogicTest.Model.CelestialBodies;

internal class Star : BasicOrbitalSystem<Planet>
{
    public Star(string name, double mass, IEnumerable<IOrbit<Planet>> satellites) : base(name, mass, satellites) {}
}

internal abstract class Planet : BasicOrbitalSystem<PlanetaryMoon>
{
    protected Planet(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites) : base(name, mass, satellites) {}
}

internal class TerrestrialPlanet : Planet
{
    public TerrestrialPlanet(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites) : base(name, mass, satellites) {}
}

internal class HabitableTerrestrialPlanet : TerrestrialPlanet, ICanBeTerraformed
{
    private readonly List<Colony> _colonies = new();

    public HabitableTerrestrialPlanet(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites, bool readyForColonization) : base(name, mass, satellites)
    {
        ReadyForColonization = readyForColonization;
    }

    public bool ReadyForColonization { get; private set; }

    public IReadOnlyCollection<Colony> Colonies => _colonies;

    public IReadOnlyCollection<Colony> TryCreateColony(Colonizers colonizers, string colonyName)
    {
        if (ReadyForColonization && Rng.Next(0, 100) < colonizers.ColonizationSuccessRate)
        {
            var colony = new Colony(this, colonizers, colonyName);
            _colonies.Add(colony);
            return new[] {colony};
        }

        return Array.Empty<Colony>();
    }

    public bool Terraform(ITerraformer terraformer)
    {
        if (!ReadyForColonization && Rng.Next(0, 100) < terraformer.TerraformingSuccessRate)
        {
            ReadyForColonization = true;
            return true;
        }

        return false;
    }
}

internal class GasGiant : Planet
{
    public GasGiant(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites) : base(name, mass, satellites) {}
}

internal class HabitableGasGiant : GasGiant, ICanSustainLife
{
    private readonly List<Colony> _colonies = new();

    public HabitableGasGiant(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites, bool readyForColonization) : base(name, mass, satellites)
    {
        ReadyForColonization = readyForColonization;
    }

    public IReadOnlyCollection<Colony> Colonies => _colonies;

    public bool ReadyForColonization { get; }

    public IReadOnlyCollection<Colony> TryCreateColony(Colonizers colonizers, string colonyName)
    {
        if (ReadyForColonization && Rng.Next(0, 100) < colonizers.ColonizationSuccessRate)
        {
            var colony = new Colony(this, colonizers, colonyName);
            _colonies.Add(colony);
            return new[] {colony};
        }

        return Array.Empty<Colony>();
    }
}

internal class IceGiant : Planet
{
    public IceGiant(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites) : base(name, mass, satellites) {}
}

internal class DwarfPlanet : Planet
{
    public DwarfPlanet(string name, double mass, IEnumerable<IOrbit<PlanetaryMoon>> satellites) : base(name, mass, satellites) {}
}


internal class PlanetaryMoon : BasicCelestialBody
{
    public PlanetaryMoon(string name, double mass) : base(name, mass) {}
}

internal class HabitablePlanetaryMoon : PlanetaryMoon, ICanBeTerraformed
{
    private readonly List<Colony> _colonies = new();

    public HabitablePlanetaryMoon(string name, double mass) : base(name, mass)
    {
    }

    public bool ReadyForColonization { get; private set; }

    public IReadOnlyCollection<Colony> Colonies => _colonies;

    public IReadOnlyCollection<Colony> TryCreateColony(Colonizers colonizers, string colonyName)
    {
        if (ReadyForColonization && Rng.Next(0, 100) < colonizers.ColonizationSuccessRate)
        {
            var colony = new Colony(this, colonizers, colonyName);
            _colonies.Add(colony);
            return new[] { colony };
        }

        return Array.Empty<Colony>();
    }

    public bool Terraform(ITerraformer terraformer)
    {
        if (!ReadyForColonization && Rng.Next(0, 100) < terraformer.TerraformingSuccessRate)
        {
            ReadyForColonization = true;
            return true;
        }

        return false;
    }
}