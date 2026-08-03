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
        public void DoUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity)
        {
            Debug.Log("ダイスを使用");
            List<CardsManager.Card> playerCopy = new List<CardsManager.Card>(playerData.GetCard());

            if(playerCopy.Count == 0)
            {
                return;
            }

            // ランダムな一枚を選ぶ
            int targetIndex = Random.Range(0, playerCopy.Count);
            CardsManager.Card oldCard = playerCopy[targetIndex];

            CardsManager.Rank targetRank = (CardsManager.Rank)Random.Range(1, 7);

            // ランクを1～6のランダムな値に変更
            targetRank = (CardsManager.Rank)Random.Range(1, 7);

            if(!deck.TryDrawByRank(targetRank,out CardsManager.Card newCard))
            {
                // 該当ランクがないなら無視
                return;
            }

            // 元のカード山札にもどす
            deck.ReturnCard(oldCard);

            playerData.ReplaceCard(targetIndex,newCard);
            Debug.Log("ダイスの使用終了");
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}