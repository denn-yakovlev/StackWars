using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWars
{
    interface IUnit : ICloneable
    {
        int Health { get; }

        int Attack { get; }

        int Armor { get; }

        int Cost { get; }

        void TakeDamage(int damage);
    }

    abstract class Unit : IUnit
    {
        public abstract int Health { get; protected set; }

        public abstract int Attack { get; }

        public abstract int Armor { get; }

        public abstract int Cost { get; }

        public object Clone() => MemberwiseClone();

        public virtual void TakeDamage(int damage)
        {
            Health -= Math.Max(0, damage - Armor);
        }

        public override string ToString() => $"{GetType().Name}({Health} HP)";
    }

    class Infantry : Unit // низк. цена, маленький урон, мал. защита
    {
        public override int Health { get; protected set; } = 100;

        public override int Attack { get; } = 30;

        public override int Armor { get; } = 10;

        public override int Cost { get; } = 100;
    }

    class Knight : Unit // выс. цена, выс.урон и защита
    {
        public override int Health { get; protected set; } = 150;

        public override int Attack { get; } = 50;

        public override int Armor { get; } = 25;

        public override int Cost { get; } = 200;
    }

    interface IUnitFactory
    {
        IUnit Create();
    }
     
    class UnitFactory<T> : IUnitFactory 
        where T: IUnit, new()
    {
        public IUnit Create() => new T();
    }

    interface IArmyFactory
    {
        IEnumerable<IUnit> Create(int maxCost);
    }

    abstract class ArmyFactory : IArmyFactory
    {
        protected abstract Func<IUnit> UnitSupplier { get; }

        public virtual IEnumerable<IUnit> Create(int maxCost)
        {
            var cost = 0;
            var result = new List<IUnit>();
            while (true)
            {
                IUnit unit = UnitSupplier();
                if (unit != null && unit.Cost + cost <= maxCost)
                {
                    result.Add(unit);
                    cost += unit.Cost;
                }      
                else
                    break;
            }
            return result;
        }
    }

    abstract class FromUnitFactoriesCreator : ArmyFactory
    {
        protected IEnumerable<IUnitFactory> factories = new List<IUnitFactory>();

        public FromUnitFactoriesCreator(params IUnitFactory[] factories)
        {
            this.factories = factories;
        }
    }

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

    class RandomArmyFactory : FromUnitFactoriesCreator
    {
        private static readonly Random _random = new Random();

        protected override Func<IUnit> UnitSupplier => () =>
            factories.ElementAt(_random.Next(0, factories.Count())).Create();

        public RandomArmyFactory(params IUnitFactory[] factories) : base(factories)
        {

        }        
    }

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
