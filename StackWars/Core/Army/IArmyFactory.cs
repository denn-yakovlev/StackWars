using System.Collections.Generic;

namespace StackWars
{
    interface IArmyFactory
    {
        IEnumerable<IUnit> Create(int maxCost);
    }
}
