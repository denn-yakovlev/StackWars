using StackWars.Core.Army;
using StackWars.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackWars.Core.Game
{
    class AllVsAllBattle : IBattleStrategy
    {
        public IList<AliveUnitsEnumerable> GetBattlers(IArmy army) =>
            new List<AliveUnitsEnumerable> { army.ToAliveUnitsEnumerable() };
    }
}
