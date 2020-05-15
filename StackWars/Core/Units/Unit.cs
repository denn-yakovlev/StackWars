using System;

namespace StackWars.Core.Units
{
    abstract class Unit : IUnit
    {
        public abstract int Health { get; protected set; }

        public abstract int Attack { get; }

        public abstract int Armor { get; }

        public abstract int Cost { get; }

        public object Clone() => MemberwiseClone();

        // Новая механика урона: любая атака вне зависимости от брони противника 
        // наносит как минимум 50% от макс.значения урона

        public virtual void TakeDamage(int damage)
        {
            Health -= Math.Max((int)(0.5 * Attack), damage - Armor);
        }

        protected Guid guid = Guid.NewGuid();

        public override string ToString() => 
            $"{GetType().Name} ({Health} HP) #{guid.ToString().Substring(0, 8)}";       
    }
}
