using Player;
using UnityEngine;

namespace Item
{
    public class DevilcallDefinition : ItemDefinition
    {
        public static readonly DevilcallDefinition INSTANCE = new();

        public string Name
        {
            get { return "Devil Call"; }
        }

        /// <summary>
        /// コモンを基準とする
        /// </summary>
        public int Value
        {
            get { return 100000; }
        }

        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
        }

        private DevilcallDefinition() { }
    }
}