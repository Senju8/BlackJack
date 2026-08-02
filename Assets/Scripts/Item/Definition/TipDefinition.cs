using Player;

namespace Item
{
    public class TipDefinition : ItemDefinition
    {
        public static readonly TipDefinition INSTANCE = new();

        public string Name
        {
            get { return "Tip"; }
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

        private TipDefinition() { }
    }
}