using LogicTest.Model.Colonization;

namespace LogicTest.Model
{
    internal interface ICanSustainLife : ICelestialBody
    {
        bool ReadyForColonization { get; }
        IReadOnlyCollection<Colony> Colonies { get; }
        IReadOnlyCollection<Colony> TryCreateColony(Colonizers colonizers, string colonyName);
    }
}
