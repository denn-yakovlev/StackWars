using StackWars.Core.Army;
using System.Collections.Generic;

namespace StackWars.Core.Units
{
    interface IReferencingEnemies
    {
        IArmy? Enemies { get; set; }
    }
}