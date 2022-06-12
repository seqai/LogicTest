using LogicTest.Model;
using LogicTest.Model.CelestialBodies;
using LogicTest.Model.Colonization;
using Microsoft.VisualBasic;

var earth = new Orbit<HabitableTerrestrialPlanet>(new HabitableTerrestrialPlanet(
        "Earth", 
        5.97e24, 
        new [] { new Orbit<HabitablePlanetaryMoon>(new HabitablePlanetaryMoon("Moon", 0.073e24), 27.3, 0.386e6) }, true), 
    365.2,
    149.6e6);

earth.OrbitingBody.TryCreateColony(new Colonizers("Humanity", 1000, 1000, _ => { }), "Human Civilization");

var system = new Star("Sun", 0, new IOrbit<Planet>[]
{
    earth,
    new Orbit<TerrestrialPlanet>(new TerrestrialPlanet("Mercury", 0.33e24, Array.Empty<IOrbit<PlanetaryMoon>>()), 88, 57.9e6),
    new Orbit<TerrestrialPlanet>(new TerrestrialPlanet("Venus", 4.87e24, Array.Empty<IOrbit<PlanetaryMoon>>()), 224.7, 108.2e6),
    new Orbit<HabitableTerrestrialPlanet>(new HabitableTerrestrialPlanet(
            "Mars", 
            0.642e24, 
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<HabitablePlanetaryMoon>(new HabitablePlanetaryMoon("Phobos", 1.0659e16), 0.31, 9376),
                new Orbit<PlanetaryMoon>(new PlanetaryMoon("Deimos", 14762e15), 1.263, 23463)
            },
            false),
        687, 
        228e6),
    new Orbit<HabitableGasGiant>(new HabitableGasGiant(
            "Jupiter", 
            1898e24, 
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<HabitablePlanetaryMoon>(new HabitablePlanetaryMoon("Europa", 1.0659e16), 0.31, 9376),
            },
            false), 
        4331, 
        778.5e6),
    new Orbit<GasGiant>(new GasGiant(
            "Saturn", 
            568e24, 
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<HabitablePlanetaryMoon>(new HabitablePlanetaryMoon("Titan", 1.3452e23), 14, 1.221e6),
            }),
        10747, 
        1432e6),
    new Orbit<IceGiant>(new IceGiant(
            "Uranus", 
            86.8e24,
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<PlanetaryMoon>(new PlanetaryMoon("Titania", 3.4e21), 8.7, 435910),
            }), 
        30589, 
        2867e6),
    new Orbit<IceGiant>(new IceGiant(
            "Neptune", 
            102e24,
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<PlanetaryMoon>(new PlanetaryMoon("Triton", 2.13e22), 5.87, 354759),
            }),  
        59800,
        4515e6),
    new Orbit<DwarfPlanet>(new DwarfPlanet(
            "Pluto",
            0.013e24,
            new IOrbit<PlanetaryMoon>[]
            {
                new Orbit<HabitablePlanetaryMoon>(new HabitablePlanetaryMoon("Charon", 1.586e21), 6.38, 19591)
            }),  
        90560, 
        5906.4e6),
});


var delay = 100;
var colonizationJournal = new List<string>();

var speciesName = "";
while (true)
{
    Console.WriteLine("Enter Species Name: ");
    speciesName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(speciesName))
    {
        break;
    }
}

var terraformingSuccessRate = 0;
while (true)
{
    Console.WriteLine("Enter terraforming success rate (0-100): ");
    var terraformingSuccessRateInput = Console.ReadLine();

    if (int.TryParse(terraformingSuccessRateInput, out terraformingSuccessRate) && terraformingSuccessRate is >= 0 and <= 100)
    {
        break;
    }

    Console.WriteLine("Sorry, invalid input, please enter valid integer number from 0 to 100");
}

var colonizationSuccessRate = 0;
while (true)
{
    Console.WriteLine("Enter colonization success Rate (0-100): ");
    var colonizationSucessRateInput = Console.ReadLine();

    if (int.TryParse(colonizationSucessRateInput, out colonizationSuccessRate) && colonizationSuccessRate is >= 0 and <= 100)
    {
        break;
    }

    Console.WriteLine("Sorry, invalid input, please enter valid integer number from 0 to 100");
}


var colonizers = new Colonizers(speciesName, terraformingSuccessRate, colonizationSuccessRate, record => colonizationJournal.Add(record));
var colonies = colonizers.Colonize(system).ToList();


foreach (var (record, id) in colonizationJournal.Select((x, i) => (x, i + 1)))
{
    Console.WriteLine($"Record {id}:\t{record}");
    await Task.Delay(delay);
}

Console.WriteLine();
Console.WriteLine("COLONIZATION REPORT");
Console.WriteLine($"Total colonies: {colonies.Count}");
foreach (var (celestialBody, _, name) in colonies)
{
    Console.WriteLine($"Colony {name} on {celestialBody.Name} ({celestialBody.GetType().Name})");
}

Console.ReadLine();
