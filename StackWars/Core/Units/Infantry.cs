using StackWars.Core.Army;
using System;
using System.Linq;

namespace StackWars.Core.Units
{
    class Infantry : Unit, IReferencingAllies, ISpecialAction // низк. цена, маленький урон, мал. защита
    {
        public override int MaxHealth { get; protected set; } = 100;

        public override int Attack { get; protected set; } = 35;

        public override int Armor { get; protected set; } = 10;

        public override int Cost { get; protected set; } = 100;

        public IArmy? Allies { get; set; }

        IUnit?[] AlliesToEquip => new IUnit?[]
        {
            Allies.ElementAtOrDefault(((IReferencingAllies)this).PositionAmongAllies - 1),
            Allies.ElementAtOrDefault(((IReferencingAllies)this).PositionAmongAllies + 1)
        };

        private static Random _random = new Random();

        public void DoSpecialAction()
        {
            var equipment = new IEquipmentItem[]
            {
                new Helmet(),
                new Spear(),
                new Shield(),
                new Horse()
            };
            for(var i = 0; i < AlliesToEquip.Length; i++)
            {
                var ally = AlliesToEquip[i];
                if (ally != null && ally is IEquipable equipable)
                {
                    bool hasEquipped = false;
                    while (!hasEquipped)
                    {
                        try
                        {
                            var equiped = equipable.EquipWith(equipment[_random.Next(0, equipment.Length)]);
                            break;
                        }
                        catch (EquipmentFailedException)
                        {
                            hasEquipped = true;
                        }
                    }
                }
            }
        }
    }
}
