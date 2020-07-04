using System.Collections.Generic;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    interface IArmyFactory
    {
        ICollection<IUnit> Create(int maxCost);
    }
}
