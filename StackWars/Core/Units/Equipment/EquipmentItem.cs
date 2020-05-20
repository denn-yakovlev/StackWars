namespace StackWars.Core.Units
{
    abstract class EquipmentItem : IEquipmentItem
    {
        public abstract int HealthBonus { get; }
        public abstract int AttackBonus { get; }
        public abstract int ArmorBonus { get; }
        public virtual int Cost { get; } = 0;

        public override string ToString() =>
            $"{GetType().Name}({HealthBonus} HP, {AttackBonus} AD, {ArmorBonus} Armor)";
    }

    class Helmet : EquipmentItem
    {
        public override int HealthBonus => 0;

        public override int AttackBonus => 0;

        public override int ArmorBonus => 5;
    }

    class Spear : EquipmentItem
    {
        public override int HealthBonus => 0;

        public override int AttackBonus => 10;

        public override int ArmorBonus => 0;
    }

    class Shield : EquipmentItem
    {
        public override int HealthBonus => 0;

        public override int AttackBonus => -5;

        public override int ArmorBonus => 10;
    }

    class Horse : EquipmentItem
    {
        public override int HealthBonus => 25;

        public override int AttackBonus => 5;

        public override int ArmorBonus => -15;
    }
}
