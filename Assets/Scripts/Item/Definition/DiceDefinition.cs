using Player;

namespace Item
{
    public class DiceDefinition : ItemDefinition
    {
        public static readonly DiceDefinition INSTANCE = new();

        public string Name
        {
            get { return "Dice"; }
        }

        /// <summary>
        /// コモンを基準とする
        /// </summary>
        public int Value
        {
            get { return 5000; }
        }

        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
        }

        private DiceDefinition() { }
    }
}