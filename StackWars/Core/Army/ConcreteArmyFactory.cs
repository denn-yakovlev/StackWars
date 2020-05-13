using System;
using System.Collections.Generic;

namespace StackWars
{
    class ConcreteArmyFactory : FromUnitFactoriesCreator
    {
        private IEnumerator<IUnitFactory> _factoriesEnumerator;

        protected override Func<IUnit> UnitSupplier => () =>
        {
            bool hasNext = _factoriesEnumerator.MoveNext();
            return hasNext ?_factoriesEnumerator.Current.Create() : null;
        };

        public ConcreteArmyFactory(params IUnitFactory[] factories) : base(factories)
        {
            _factoriesEnumerator = ((IEnumerable<IUnitFactory>)factories).GetEnumerator();
        }
    }
}
