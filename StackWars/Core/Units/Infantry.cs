namespace StackWars.Core.Units
{
    class Infantry : Unit // низк. цена, маленький урон, мал. защита
    {
        public override int Health { get; protected set; } = 100;

        public override int Attack { get; protected set; } = 35;

        public override int Armor { get; protected set; } = 10;

        public override int Cost { get; protected set; } = 100;
    }
}
