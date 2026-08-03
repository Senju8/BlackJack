using Cards;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// アイテム:サイコロ
    /// </summary>
    public class DiceDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Dice"; }
        }

        public int Value
        {
            get { return 5000; }
        }

        /// <summary>
        /// プレイヤのランダムなカードを1～6にする
        /// </summary>
        /// <param name="playerData"></param>
        /// <param name="dealerData"></param>
        /// <param name="rarity"></param>
        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
            List<CardsManager.Card> playerCopy = new List<CardsManager.Card>(playerData.GetCard());

            if(playerCopy.Count == 0)
            {
                return;
            }

            // ランダムな一枚を選ぶ
            int targetIndex = Random.Range(0, playerCopy.Count);
            CardsManager.Card targetCard = playerCopy[targetIndex];

            // ランクを1～6のランダムな値に変更
            targetCard.rank = (CardsManager.Rank)Random.Range(1, 7);

            playerCopy[targetIndex] = targetCard;

            playerData.SetCard(playerCopy);
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}