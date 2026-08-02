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
        /// ディーラーが最初に2枚カードを引く際の動き
        /// </summary>
        public void DrawInitialCards()
        {
            // 一枚目のカードは見える(表向き)
            var card1 = deck.DrawCard();
            dealerCards.Add(card1);
            handView.AddCard(card1, true);

            // 二枚目のカードは見えない(裏向き)
            var card2 = deck.DrawCard();
            dealerCards.Add(card2);
            handView.AddCard(card2, false);

            dealerData.SetCard(dealerCards);
            int score = ScoreCalclator.CalculateScore(dealerCards);
            dealerData.SetScore(score);
        }

        public void DrawCard(int amount,bool isOpen)
        {
            for (int i = 0; i < amount; i++)
            {
                CardsManager.Card card = deck.DrawCard();
                dealerCards.Add(card);
                handView.AddCard(card,isOpen);
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
            DrawCard(1,false);
        }

        public void Stand()
        {
            dealerData.SetIsPlaying(false);
        }

        /// <summary>
        /// ディーラーのカードを全て表向きにする
        /// </summary>
        public void CardsOpen()
        {
            for(int i=0;i<dealerCards.Count;i++)
            {
                handView.SetCardFaceUp(i, true);
            }
        }

        /// <summary>
        /// ディーラーのカードを全て削除する
        /// </summary>
        public void ClearCards()
        {
            handView.ClearHand();
        }
    }
}