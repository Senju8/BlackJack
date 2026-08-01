using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class DealerCards : MonoBehaviour
    {
        [SerializeField]
        private Deck deck;

        [SerializeField]
        private HandView handView;

        // デモ:一旦ここでDealerDataを生成する
        private DealerData dealerData = GameManager.INSTANCE.dealerData;

        private List<CardsManager.Card> dealerCards = new List<CardsManager.Card>();

        public void Setup(DealerData data, Deck deck)
        {
            this.dealerData = data;
            this.deck = deck;
            dealerCards.Clear();
            dealerCards = dealerData.GetCard();
        }

        /// <summary>
        /// ディーラーの更新処理
        /// </summary>
        public void OnUpdate()
        {
            
        }

        public void DrawCard(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CardsManager.Card card = deck.DrawCard();
                dealerCards.Add(card);
                handView.AddCard(card);
            }

            // データに適応
            dealerData.SetCard(dealerCards);

            // スコアを計算してセット
            int score = ScoreCalclator.CalculateScore(dealerCards);
            dealerData.SetScore(score);

            if (ScoreCalclator.IsBurst(dealerCards))
            {
                dealerData.SetIsPlaying(false);
            }
        }

        public void Hit()
        {
            DrawCard(1);
        }

        public void Stand()
        {
            dealerData.SetIsPlaying(false);
        }
    }
}