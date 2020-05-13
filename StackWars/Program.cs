using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Core.Army;
using StackWars.Core.Units;

namespace StackWars
{

    class Program
    {
        static void Main(string[] args)
        {
            IUnitFactory infantryFactory = new UnitFactory<Infantry>();
            IUnitFactory knightFactory = new UnitFactory<Knight>();
            
            var armies = new IEnumerable<IUnit>[6];
            IArmyFactory armyFactory = new RandomArmyFactory(
                infantryFactory, knightFactory, knightFactory, infantryFactory, knightFactory
                );

            Console.WriteLine("---Random armies(max 1000 cost)");
            for (int i = 0; i < 4; i++)
            {
                armies[i] = armyFactory.Create(1000);
                Console.WriteLine(
                    string.Join(", ", armies[i]) + 
                    $": OVERALL COSTS {armies[i].Sum(unit => unit.Cost)}"
                    );
            }

            armyFactory = new ConcreteArmyFactory(infantryFactory, knightFactory, infantryFactory);

            Console.WriteLine();
            Console.WriteLine("---Concrete army(max 600 cost)");

            armies[4] = armyFactory.Create(600);
            Console.WriteLine(
                    string.Join(", ", armies[4]) +
                    $": OVERALL COSTS {armies[4].Sum(unit => unit.Cost)}"
                    );

            armyFactory = new UnitsCloningFactory(
                infantryFactory.Create(),
                knightFactory.Create(),
                infantryFactory.Create()
                );

            Console.WriteLine();
            Console.WriteLine("--Units cloner(max 300 cost)");
            armies[5] = armyFactory.Create(300);
            Console.WriteLine(
                string.Join(", ", armies[5]) +
                $": OVERALL COSTS {armies[5].Sum(unit => unit.Cost)}"
                );

        }
    }
}
