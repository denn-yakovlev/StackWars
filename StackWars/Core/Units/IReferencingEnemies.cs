using System.Collections.Generic;

namespace StackWars.Core.Units
{
    interface IReferencingEnemies
    {
        IEnumerable<IUnit> Enemies { get; set; }
    }
}