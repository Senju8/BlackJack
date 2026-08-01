using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤークラス
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Deck deck;

        private List<CardsManager.Card> playerCards = new List<CardsManager.Card>();

        [SerializeField]
        private CardsView cardsViewPrefab;

        [SerializeField]
        private CardsSprite cardsSprite;

        /// <summary>
        /// カードを並べる位置のTransform
        /// </summary>
        [SerializeField]
        private Transform cardPosition;


        /// <summary>
        /// ボタンを押したときにカードを引く処理
        /// DeckクラスのDrawCardメソッドを呼び出す
        /// </summary>
        /// <param name="count">引くカードの枚数</param>
        public void DrawCard(int count)
        {
            for(int i=0;i<count;i++)
            {
                var card = deck.DrawCard();
                playerCards.Add(card);

                CardsView view = Instantiate(cardsViewPrefab, cardPosition);
                view.Setup(card, cardsSprite);
            }
        }

        /// <summary>
        /// プレイヤの手札を表示
        /// </summary>
        public void ShowPlayerCards()
        {
            Debug.Log("プレイヤーの手札:");
            foreach (var card in playerCards)
            {
                Debug.Log(card.suit + " " + card.rank);
            }
        }
    }
}