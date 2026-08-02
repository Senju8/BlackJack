using Cards;
using Item;
using System;
using System.Collections.Generic;

namespace Player
{
    /// <summary>
    /// プレイヤークラス
    /// </summary>
    public class PlayerData
    {
        public readonly GameManager gameManager;

        /// <summary>
        /// プレイ全体用
        /// </summary>
        private int values = 0; // プレイヤの所持金額

        /// <summary>
        /// ブラックジャック用
        /// </summary>
        private List<CardsManager.Card> playerCards = new List<CardsManager.Card>();    // プレイヤの札
        private int playerScore = 0;    // プレイヤのスコア
        private List<ItemData> playerItems = new List<ItemData>();   // プレイヤのアイテム

        /// <summary>
        /// プレイヤがゲームを続けられるかの判定変数
        /// 
        /// スタンドを行うとfalseに
        /// </summary>
        private bool isPlaying = true;

        /// <summary>
        /// 現在確定しているベット額
        /// </summary>
        private int betAmount = 0;


        public PlayerData(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        // プレイヤの所持金額
        public int GetValues()
        {
            return values;
        }

        public void SetValues(int value)
        {
            values = value;
        }

        // プレイヤの札
        public List<CardsManager.Card> GetCard()
        {
            return playerCards;
        }

        // プレイヤの札をセット
        public void SetCard(List<CardsManager.Card> cards)
        {
            playerCards = cards;
        }

        /// <summary>
        /// スコアの変更があった際に発火するイベント
        /// </summary>
        public event Action<int> OnScoreChanded;

        // プレイヤのスコア
        public int GetScore()
        {
            return playerScore;
        }

        public void SetScore(int score)
        {
            playerScore = score;
            OnScoreChanded?.Invoke(playerScore);    //イベント発火
        }

        public int GetBet()
        {
            return betAmount;
        }

        /// <summary>
        /// ベット額を確定する
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool TryConfirmBet(int amount)
        {
            if(amount <= 0 || amount > values)
            {
                return false;
            }

            values -= amount;
            betAmount = amount;

            return true;
        }

        /// <summary>
        /// ベット額のリセット
        /// </summary>
        public void ResetBet()
        {
            betAmount = 0;
        }

        // プレイヤのプレイ状態
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