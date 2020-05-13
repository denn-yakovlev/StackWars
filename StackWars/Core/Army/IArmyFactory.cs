using System.Collections.Generic;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    interface IArmyFactory
    {
        IEnumerable<IUnit> Create(int maxCost);
    }
}
