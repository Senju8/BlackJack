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
            float addMulutiper = 0.5f;

            switch(rarity)
            {
                case 1:
                    addMulutiper = 2.0f;
                    break;

                case 2:
                    addMulutiper = 4.0f;
                    break;

                case 3:
                    addMulutiper = 8.0f;
                    break;

                case 4:
                    addMulutiper = 16.0f;
                    break;

                case 5:
                    addMulutiper = 32.0f;
                    break;
            }
            playerData.PayoutMultiplier.SetBonus("item_tip", addMulutiper);
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}