using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Core.Army;
using StackWars.Core.Units;
using StackWars.Core.Game;

namespace StackWars
{
    class Program
    { 
        static void Main(string[] args)
        {
            IEnumerable<IUnit> army1 = new List<IUnit>();
            IEnumerable<IUnit> army2 = new List<IUnit>();

            IUnitFactory infFactory = new UnitFactory<Infantry>();
            IUnitFactory knFactory = new UnitFactory<Knight>();
            IUnitFactory archFactory = new UnitFactory<Archer>();

            army1 = new RandomArmyFactory(
                infFactory, knFactory, archFactory
                ).Create(600);

            army2 = new RandomArmyFactory(
                infFactory, knFactory, archFactory
                ).Create(600);

            IGame game = new Game(army1, army2);
            game.TurnToEnd();
        }
    }
}
