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
            IUnitFactory infFactory = new UnitFactory<Infantry>();
            IUnitFactory knFactory = new UnitFactory<Knight>();
            IUnitFactory archFactory = new UnitFactory<Archer>();
            IUnitFactory healerFactory = new UnitFactory<Healer>();

            IBattleStrategy strategy;

            Console.WriteLine("Стратегия боя?");
            Console.WriteLine("1. Один на один");
            Console.WriteLine("2. Три на три");
            Console.WriteLine("3. Стенка на стенку");
            char keyChar;
            do
            {
                keyChar = Console.ReadKey(true).KeyChar;
            } while (keyChar <= '1' && keyChar >= '3');
            switch (keyChar)
            {
                case '1':
                    {
                        strategy = new OneVsOneBattle();
                        break;
                    }
                case '2':
                    {
                        strategy = new ThreeVsThreeBattle();
                        break;
                    }
                case '3':
                    {
                        strategy = new AllVsAllBattle();
                        break;
                    }
                default:
                    {
                        strategy = new OneVsOneBattle();
                        break;
                    }
            }

            Console.WriteLine();
            Console.WriteLine("Введите стоимость армий: ");

            IArmy army1;
            IArmy army2;

            while(true)
            {   
                int cost = PromptArmiesCost();

                army1 = new StackArmy(new RandomArmyFactory(
                    infFactory, knFactory, archFactory, healerFactory
                    ), cost);

                army2 = new StackArmy(new RandomArmyFactory(
                    infFactory, knFactory, archFactory, healerFactory
                    ), cost);
                if (army1.Count == 0 || army2.Count == 0)
                    Console.WriteLine("Слишком малая сумма, введите ещё раз");
                else break;
            }

            Console.WriteLine();
            Console.WriteLine("Первая армия ");
            Console.WriteLine();
            Console.WriteLine(army1);
            Console.WriteLine();
            Console.WriteLine("Вторая армия ");
            Console.WriteLine();
            Console.WriteLine(army2);

            IGame game = new Game(army1, army2, strategy);

            Console.WriteLine();
            Console.WriteLine("Управление: ENTER - сделать ход , SPACE - бой до конца, ESC- выход");

            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {
                switch (key)
                {
                    case ConsoleKey.Enter:
                        {
                            game.Turn();
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            game.TurnToEnd();
                            break;
                        }
                    default: break;
                }
                key = Console.ReadKey().Key;
            }
            

        }

        private static int PromptArmiesCost()
        {
            bool isNum;
            int cost;
            do
            {
                isNum = int.TryParse(Console.ReadLine(), out cost);
            } while (!isNum);
            return cost;
        }
    }
}
