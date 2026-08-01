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

        // デモ:プレイヤの札をセット
        public void SetCard(List<CardsManager.Card> cards)
        {
            playerCards = cards;
        }

        // プレイヤのスコア
        public int GetScore()
        {
            return playerScore;
        }

        public void SetScore(int score)
        {
            playerScore = score;
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