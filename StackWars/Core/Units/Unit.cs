using System;

namespace StackWars.Core.Units
{
    abstract class Unit : IUnit
    {
        public abstract int MaxHealth { get; protected set; }

        public abstract int Attack { get; protected set; }

        public abstract int Armor { get; protected set; }

        public abstract int Cost { get; protected set; }

        public int Health { get; protected set; }

        protected Unit()
        {
            Health = MaxHealth;
        }

        public object Clone() => MemberwiseClone();

        // Новая механика урона: любая атака вне зависимости от брони противника 
        // наносит как минимум 50% от макс.значения урона

        public virtual void TakeDamage(int damage)
        {
            Health -= Math.Max((int)(0.5 * Attack), damage - Armor);
        }

        public virtual void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
        }

        protected Guid guid = Guid.NewGuid();

        private string _ShortGuid => guid.ToString().Substring(0, 8);   

        public override string ToString() => 
            $"{GetType().Name} ({Health} HP) #{_ShortGuid}";       
    }
}
