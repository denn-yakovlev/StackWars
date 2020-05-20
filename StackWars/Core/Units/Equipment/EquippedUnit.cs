namespace StackWars.Core.Units
{
    class EquippedUnit : Unit, IEquipable
    {
        public override int Health { get; protected set; }
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
                throw new EquipmentFailed(equipment, wrappee);
            _equipment = equipment;
            Health = _wrappee.Health + equipment.HealthBonus;
            Attack = _wrappee.Attack + equipment.AttackBonus;
            Armor = _wrappee.Armor + equipment.ArmorBonus;
            Cost = _wrappee.Cost + equipment.Cost;
        }
        
        public IEquipable EquipWith(IEquipmentItem item) => new EquippedUnit(this, item);
    }
}
