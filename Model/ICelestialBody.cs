using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest.Model
{
    internal interface ICelestialBody
    {
        string Name { get; }
        double Mass { get; }
        // assuming storing some picture id
        // can be byte[] if we want for some reason store the picture itself
        string Picture { get; }
    }
}
