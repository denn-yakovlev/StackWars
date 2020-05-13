using System.Collections.Generic;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    abstract class FromUnitFactoriesCreator : ArmyFactory
    {
        protected IEnumerable<IUnitFactory> factories = new List<IUnitFactory>();

        public FromUnitFactoriesCreator(params IUnitFactory[] factories)
        {
            this.factories = factories;
        }
    }
}
