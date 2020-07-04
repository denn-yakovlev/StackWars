using StackWars.Core.Army;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StackWars.Core.Units
{
    class Healer : Unit, ISpecialAction, IReferencingAllies, IHealable
    {
        public override int MaxHealth { get; protected set; } = 75;

        public override int Attack { get; protected set; } = 25;
        
        public override int Armor { get; protected set; } = 0;

        public override int Cost { get; protected set; } = 135;

        public IArmy? Allies { get; set; }

        public int PositionAmongAllies => Allies.TakeWhile(u => u != this).Count();

        IUnit?[] AlliesToHeal => new IUnit?[] 
        {
            Allies.ElementAtOrDefault(PositionAmongAllies - 1),
            Allies.ElementAtOrDefault(PositionAmongAllies + 1)
        };

        const int healStrength = 30;

        const double healChance = 0.5;

        static Random _random = new Random();

        public bool HasChanceToHeal => _random.NextDouble() <= healChance;

        public void DoSpecialAction()
        {
            foreach (var ally in AlliesToHeal)
            {
                Logger.Instance.Log($"{this} пытается применить спец. способность.");
                if (ally != null && ally is IHealable healable)
                {
                    if (HasChanceToHeal)
                    {
                        var healableHpBefore = ally.Health;
                        healable.Heal(healStrength);
                        var healableHpAfter = ally.Health;
                        Logger.Instance.Log(
                            $"{this} восполняет {healableHpAfter - healableHpBefore} ед. здоровья юниту {ally}"
                            );
                    }
                    else
                    {
                        Logger.Instance.Log($"{this} не удалось вылечить союзника");
                    }
                }

                    
            }

        }
    }
}
