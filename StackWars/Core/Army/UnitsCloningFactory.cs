using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWars
{
    class UnitsCloningFactory : ArmyFactory
    {
        private readonly IEnumerable<IUnit> _prototypes = new List<IUnit>();

        private IEnumerator<IUnit> _prototypesEnumerator;

        protected override Func<IUnit> UnitSupplier => () =>
        {
            bool hasNext = _prototypesEnumerator.MoveNext();
            return hasNext ? _prototypesEnumerator.Current : null;
        };

        public UnitsCloningFactory(params IUnit[] prototypes)
        {
            _prototypes = prototypes.OrderBy(proto => proto.Cost);
            _prototypesEnumerator = _prototypes.GetEnumerator();
        }

        public void AddPrototype(IUnit prototype) =>
            _prototypes.Append(prototype);     
        
    }
}
