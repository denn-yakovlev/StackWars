using StackWars.Core.Army;
using StackWars.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackWars.Core.Game
{
    class OneVsOneBattle : IBattleStrategy
    {
        public IList<AliveUnitsEnumerable> GetBattlers(IArmy army) =>
            army.Select(u => new AliveUnitsEnumerable(new IUnit[] { u })).ToList();
    }
}
