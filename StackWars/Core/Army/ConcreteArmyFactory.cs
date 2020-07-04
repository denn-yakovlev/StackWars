using System;
using System.Collections.Generic;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    class ConcreteArmyFactory : FromUnitFactoriesCreator
    {
        private IEnumerator<IUnitFactory> _factoriesEnumerator;

        protected override IUnit? GetUnit()
        {
            bool hasNext = _factoriesEnumerator.MoveNext();
            return hasNext ?_factoriesEnumerator.Current.Create() : null;
        }

        public ConcreteArmyFactory(params IUnitFactory[] factories) : base(factories)
        {
            _factoriesEnumerator = ((IEnumerable<IUnitFactory>)factories).GetEnumerator();
        }
    }
}
