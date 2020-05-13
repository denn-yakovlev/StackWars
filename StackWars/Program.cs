using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace StackWars
{
    interface IUnit: ICloneable
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
        protected IEnumerable<IUnitFactory> factories;

        public ArmyFactory(params IUnitFactory[] factories)
        {
            this.factories = factories;
        }

        public abstract IEnumerable<IUnit> Create(int maxCost);
    }

    class ConcreteArmyFactory : ArmyFactory
    {
        public ConcreteArmyFactory(params IUnitFactory[] factories) : 
            base(factories) { }

        public override IEnumerable<IUnit> Create(int maxCost)
        {
            var cost = 0;
            var result = new List<IUnit>();
            foreach (var factory in factories)
            {
                IUnit unit = factory.Create();
                cost += unit.Cost;
                if (cost > maxCost)
                    break;
                else
                    result.Add(unit);
            }
            return result;
        }
    }

    class RandomArmyFactory : ArmyFactory
    {
        private static readonly Random _random = new Random();

        public RandomArmyFactory(params IUnitFactory[] factories) : 
            base(factories) { }

        public override IEnumerable<IUnit> Create(int maxCost)
        {
            var cost = 0;
            var result = new List<IUnit>();
            while (true)
            {
                IUnit unit = factories.ElementAt(_random.Next(0, factories.Count())).Create();
                cost += unit.Cost;
                if (cost > maxCost)
                    break;
                else
                    result.Add(unit);
            }
            return result;
        }
    }

    class UnitsCloner : IArmyFactory
    {
        private readonly IEnumerable<IUnit> _prototypes;

        public UnitsCloner(params IUnit[] prototypes)
        {
            _prototypes = prototypes.OrderBy(proto => proto.Cost);
        }

        public void AddPrototype(IUnit prototype) =>
            _prototypes.Append(prototype);

        public IEnumerable<IUnit> Create(int maxCost)
        {
            var cost = 0;
            var result = new List<IUnit>();
            foreach (var proto in _prototypes)
            {
                IUnit unit = (IUnit)proto.Clone();
                cost += unit.Cost;
                if (cost > maxCost)
                    break;
                else
                    result.Add(unit);
            }
            return result;
        }
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
            Console.WriteLine("---Concrete army(max 300 cost)");

            armies[4] = armyFactory.Create(300);
            Console.WriteLine(
                    string.Join(", ", armies[4]) +
                    $": OVERALL COSTS {armies[4].Sum(unit => unit.Cost)}"
                    );

            armyFactory = new UnitsCloner(
                infantryFactory.Create(),
                knightFactory.Create(),
                infantryFactory.Create()
                );

            Console.WriteLine();
            Console.WriteLine("--Units cloner(max 300 cost)");
            armies[5] = armyFactory.Create(300);
            Console.WriteLine(
                string.Join(", ", armies[4]) +
                $": OVERALL COSTS {armies[4].Sum(unit => unit.Cost)}"
                );
        }
    }
}
