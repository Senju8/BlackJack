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
        private Deck deck;

        [SerializeField]
        private HandView handView;

        //　デモ:一旦ここでPlayerDataを生成する
        private PlayerData playerData = new PlayerData();

        /// <summary>
        /// プレイヤの札
        /// </summary>
        private List<CardsManager.Card> playerCards = new List<CardsManager.Card>();

        public void Setup(PlayerData data,Deck deck)
        {
            this.playerData = data;
            this.deck = deck;
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

            // スコアを計算してセット
            int score = ScoreCalclator.CalculateScore(playerCards);
            playerData.SetScore(score);

            if(ScoreCalclator.IsBurst(playerCards))
            {
                playerData.SetIsPlaying(false);
            }
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