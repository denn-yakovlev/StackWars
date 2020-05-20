namespace StackWars.Core.Units
{
    interface IEquipmentItem
    {
        int HealthBonus { get; }

        int AttackBonus { get; }

        int ArmorBonus { get; }

        int Cost { get; }
    }
}