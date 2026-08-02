using Player;
using UnityEngine;

namespace Item
{
    public class ContractDefinition : ItemDefinition
    {
        public static readonly ContractDefinition INSTANCE = new();

        public string Name
        {
            get { return "Contract"; }
        }

        /// <summary>
        /// エピックを基準とする
        /// </summary>
        public int Value
        {
            get { return 1000; }
        }

        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
        }

        private ContractDefinition() { }
    }
}
