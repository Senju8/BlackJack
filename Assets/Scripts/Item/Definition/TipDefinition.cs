using Cards;
using Player;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// アイテム:チップ
    /// </summary>
    public class TipDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Tip"; }
        }

        public int Value
        {
            get { return 5000; }
        }

        /// <summary>
        /// 倍率が増える
        /// </summary>
        /// <param name="playerData"></param>
        /// <param name="dealerData"></param>
        /// <param name="rarity"></param>
        public void DoUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity)
        {

        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}