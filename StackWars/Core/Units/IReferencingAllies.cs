using StackWars.Core.Army;
using System.Collections.Generic;
using System.Linq;

namespace StackWars.Core.Units
{
    interface IReferencingAllies
    {
        IArmy? Allies { get; set; }

        int PositionAmongAllies => Allies?.TakeWhile(u => u != this).Count() ?? -1;
    }
}