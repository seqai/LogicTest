namespace LogicTest.Model.Colonization;

internal class Colonizers : ITerraformer
{
    private readonly Action<string> _journal;
    
    private int _colonyIndex = 1;

    public Colonizers(string species, int terraformingSuccessRate, int colonizationSuccessRate, Action<string> journal)
    {
        TerraformingSuccessRate = terraformingSuccessRate;
        ColonizationSuccessRate = colonizationSuccessRate;
        Species = species;
        _journal = journal;
    }

    public string Species { get; }
    public int TerraformingSuccessRate { get; }
    public int ColonizationSuccessRate { get; }

    public IEnumerable<Colony> Colonize(ICelestialBody celestialBody)
    {
        _journal($"{Species} colonists enter {celestialBody.Name} orbit. It is a {celestialBody.GetType().Name}.");
        if (celestialBody is ICanSustainLife colonizable)
        {
            foreach (var colony in AttemptColonization(colonizable))
            {
                yield return colony;
            }
        }
        else
        {
            _journal($"{celestialBody.Name} can't sustain life. {Species} colonists continue their space travel.");
        }

        if (celestialBody is IOrbitalSystem<ICelestialBody> orbitalSystem && orbitalSystem.Satellites.Any())
        {
            foreach (var colony in orbitalSystem.Satellites.OrderBy(x => x.OrbitalPeriod).SelectMany(x =>
                     {
                         _journal($"Found a satellite of {celestialBody.Name}, {x.OrbitingBody.Name} with a mean of distance {x.MeanDistance/1e6:F}M km and an orbital period of {x.OrbitalPeriod:F} days.");
                         return Colonize(x.OrbitingBody);
                     }))
            {
                yield return colony;
            }
        }

        _journal($"{Species} colonists leave {celestialBody.Name} orbit.");

    }

    private IEnumerable<Colony> AttemptColonization(ICanSustainLife colonizableEntity)
    {
        _journal($"{colonizableEntity.Name} can sustain life!");
        if (colonizableEntity.Colonies.Any())
        {
            var alienColony = colonizableEntity.Colonies.First();
            _journal($"{colonizableEntity.Name} can already has some colonies on it ({alienColony.Name} by {alienColony.Colonizers.Species}), so {Species} colonists will leave it.");
            return Array.Empty<Colony>();
        }
        else
        {
            _journal($"{colonizableEntity.Name} does not seem to have any other species who can object {Species} colonization.");
        }

        if (!colonizableEntity.ReadyForColonization)
        {
            _journal($"{colonizableEntity.Name} does not seem to be ready for colonization. But maybe {Species} colonists can terraform it.");


            if (colonizableEntity is ICanBeTerraformed terraformable)
            {
                var result = terraformable.Terraform(this);
                _journal(
                    $"{Species} attempted to terraform {colonizableEntity.Name} and {(result ? "succeeded" : "failed")}.");
                if (!result)
                {
                    _journal( $"After failed terraformation attempt {Species} colonists have no choice but to leave {colonizableEntity.Name}.");
                    return Array.Empty<Colony>();
                }
            }
            else
            {
                _journal( $"Even if {colonizableEntity.Name} seem to be able to sustain life it can't be terraformed so {Species} colonists leave it.");
                return Array.Empty<Colony>();
            }

            if (!colonizableEntity.ReadyForColonization)
            {
                _journal( $"Even after successful terraformation {colonizableEntity.Name} {Species} colonists still can't colonize it, so they leave it.");
                return Array.Empty<Colony>();
            }
        }

        _journal( $"{colonizableEntity.Name} is ready for colonization. {Species} colonists will try to establish a colony there.");
        var colonies = colonizableEntity.TryCreateColony(this, $"{Species} Colony #{_colonyIndex}");
        if (!colonies.Any())
        {
            _journal( $"{Species} colonists could not found a new colony on {colonizableEntity.Name}. They will leave it.");
            return Array.Empty<Colony>();
        }

        var colony = colonies.First();
        _journal( $"{Species} found a new colony named {colony.Name} on {colonizableEntity.Name}.");
        _colonyIndex++;
        return colonies;
    }
}

internal record Colony(ICelestialBody Location, Colonizers Colonizers, string Name);