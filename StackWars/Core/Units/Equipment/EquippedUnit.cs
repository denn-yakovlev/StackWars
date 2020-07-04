using System;

namespace StackWars.Core.Units
{
    class EquippedUnit : Unit, IEquipable
    {
        public override int MaxHealth { get; protected set; }
        public override int Attack { get; protected set; }
        public override int Armor { get; protected set; }
        public override int Cost { get; protected set; }

        private IEquipable _wrappee;

        private IEquipmentItem _equipment; 

        private bool HasEquipped(IEquipmentItem item)
        {
            var wrappee = _wrappee;
            while (wrappee is EquippedUnit equipped)
            {
                if (equipped._equipment.GetType() == item.GetType())
                    return true;
                wrappee = equipped._wrappee;
            }
            return false;
        }

        public EquippedUnit(IEquipable wrappee, IEquipmentItem equipment)
        {
            _wrappee = wrappee;
            if (HasEquipped(equipment))
                throw new EquipmentFailedException(equipment, wrappee);
            _equipment = equipment;

            // Экипировка можеть уменьшать некоторые характеристика, но при этом они не м.б. отриц.
            MaxHealth = Math.Max(0, _wrappee.MaxHealth + equipment.HealthBonus);
            Attack = Math.Max(0, _wrappee.Attack + equipment.AttackBonus);
            Armor = Math.Max(0, _wrappee.Armor + equipment.ArmorBonus);
            Cost = Math.Max(0, _wrappee.Cost + equipment.Cost);
            Health = MaxHealth;
        }
        
        public IEquipable EquipWith(IEquipmentItem item) => new EquippedUnit(this, item);
    }
}
