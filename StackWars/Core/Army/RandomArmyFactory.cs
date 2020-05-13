using System;
using System.Linq;

namespace StackWars
{
    class RandomArmyFactory : FromUnitFactoriesCreator
    {
        private static readonly Random _random = new Random();

        protected override Func<IUnit> UnitSupplier => () =>
            factories.ElementAt(_random.Next(0, factories.Count())).Create();

        public RandomArmyFactory(params IUnitFactory[] factories) : base(factories)
        {

        }        
    }
}
