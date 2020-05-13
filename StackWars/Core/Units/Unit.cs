using System;

namespace StackWars
{
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
}
