# Logic Test

Implementation Notes:
- Existing implentation is quite rigid and hierarchical and is better suited for something with relatively simple logic, like a game rather than serious simulation
- To demonstrate current model usage a small example program is implemented, where colonists travel within solar system (from closest planet to furtherst) trying to terraform and colonize suitable planets
- Solar system is hardcoded for example purposes
- Some code related to colonization handling by individual planet types is copied and pasted for the simplicity. In reality it is a good candidate for some additional thoughts.
- To avoid too much cluttering of classes files, some related classes existing within single file: CelestialBodies.cs and Colonization.cs
- C# type system is still quite limiting to express complex relations even with pattern matching. To express oribiting bodies union types might work better, but the closest we can have is C# is marker interface which is subpar solution. In current implemetation, for example, to add an Asteroid and a Comet classes, which are orbiting a Star, some rework will be required. Colonization mechanics could be expressed more elegantly using mixins/traits. Moreover some rules within this system is very easy to break, as it is not really expressed via types: e.g. star can be of lower mass than a planet. We can only assume this as invariants or add checks for this cases. Such complex behavious can be expressed with dependent types which are far from mainstream language feature
- Real orbits are much more complex and defined by multiple parameters (e.g. Keplerian elements), which we ignore using arbitrary "mean" values (sometimes semiaxis values is used), bodies, regardles of mass, affect each others orbits and with similar mass can be orbiting each other 
