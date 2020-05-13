using System.Collections.Generic;

namespace StackWars
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
