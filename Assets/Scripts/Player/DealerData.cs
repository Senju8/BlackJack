using Cards;
using System.Collections.Generic;

namespace Player
{
    /// <summary>
    /// ディーラークラス
    /// </summary>
    public class DealerData
    {
        /// <summary>
        /// ディーラーの札
        /// </summary>
        private List<CardsManager.Card> dealerCards = new List<CardsManager.Card>();

        /// <summary>
        /// ディーラーのスコア
        /// </summary>
        private int dealerScore = 0;

        /// <summary>
        /// ディーラーがプレイ中かどうか
        /// </summary>
        private bool isPlaying = true;

        // 各変数のゲッター/セッター

        // ディーラーの札
        public List<CardsManager.Card> GetCard()
        {
            return dealerCards;
        }

        // デモ:ディーラーの札をセット
        public void SetCard(List<CardsManager.Card> cards)
        {
            dealerCards = cards;
        }

        // ディーラーのスコア        
        public int GetScore()
        {
            return dealerScore;
        }

        public void SetScore(int score)
        {
            dealerScore = score;
        }

        // ディーラーがプレイ中かどうか
        public bool GetIsPlaying()
        {
            return isPlaying;
        }

        public void SetIsPlaying(bool playing)
        {
            isPlaying = playing;
        }
    }
}