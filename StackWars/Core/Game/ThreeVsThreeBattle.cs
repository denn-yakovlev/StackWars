using StackWars.Core.Army;
using StackWars.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackWars.Core.Game
{
    class ThreeVsThreeBattle : IBattleStrategy
    {
        public IList<AliveUnitsEnumerable> GetBattlers(IArmy army)
        {
            return GenerateChunksOf3(army).ToList();
        }

        private IEnumerable<AliveUnitsEnumerable> GenerateChunksOf3(IEnumerable<IUnit> units)
        {
            while(units.Any())
            {
                yield return new AliveUnitsEnumerable(units.Take(3));
                units = units.Skip(3);
            }
        }
    }
}
