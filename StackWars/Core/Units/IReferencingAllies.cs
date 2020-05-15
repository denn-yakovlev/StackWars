using System.Collections.Generic;

namespace StackWars.Core.Units
{
    interface IReferencingAllies
    {
        IEnumerable<IUnit> Allies { get; set; }
    }
}