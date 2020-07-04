using StackWars.Core.Army;
using StackWars.Core.Units;
using System.Collections;
using System.Collections.Generic;

namespace StackWars.Core.Game
{
    public interface IBattleStrategy
    {
        IList<AliveUnitsEnumerable> GetBattlers(IArmy army);
    }
}