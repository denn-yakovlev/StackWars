using System;
using System.Linq;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    class RandomArmyFactory : FromUnitFactoriesCreator
    {
        private static readonly Random _random = new Random();

        protected override IUnit GetUnit() => 
            factories.ElementAt(_random.Next(0, factories.Count())).Create();

        public RandomArmyFactory(params IUnitFactory[] factories) : base(factories)
        {

        }        
    }
}
