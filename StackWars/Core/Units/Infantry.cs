namespace StackWars
{
    class Infantry : Unit // низк. цена, маленький урон, мал. защита
    {
        public override int Health { get; protected set; } = 100;

        public override int Attack { get; } = 30;

        public override int Armor { get; } = 10;

        public override int Cost { get; } = 100;
    }
}
