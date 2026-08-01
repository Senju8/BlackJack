using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Cards
{
    /// <summary>
    /// プレイヤーのカードの見た目や動きを管理するクラス
    /// </summary>
    public class PlayerCards : MonoBehaviour
    {
        [SerializeField]
        private HandView handView;

        private PlayerData playerData;
        private Deck deck;

        /// <summary>
        /// プレイヤの札
        /// </summary>
        private List<CardsManager.Card> playerCards = new List<CardsManager.Card>();

        public void Initialize()
        {
            playerCards.Clear();
            playerCards = playerData.GetCard();
        }

        /// <summary>
        /// カードをamount枚引き、playerCardsに追加
        /// </summary>
        /// <param name="amount"></param>
        public void DrawCard(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CardsManager.Card card = deck.DrawCard();
                playerCards.Add(card);
                handView.AddCard(card);
            }
            
            // データに適応
            playerData.SetCard(playerCards);
        }

        public void Hit()
        {
            DrawCard(1);
        }

        public void Stand()
        {
            playerData.SetIsPlaying(false);
        }
    }
}