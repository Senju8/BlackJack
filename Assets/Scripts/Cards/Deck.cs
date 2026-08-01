using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// 山札クラス
    /// </summary>
    public class Deck : MonoBehaviour
    {
        /// <summary>
        /// 山札のカードリスト
        /// </summary>
        private List<CardsManager.Card> deckCards = new List<CardsManager.Card>();

        /// <summary>
        /// 使用済みカードのリスト
        /// </summary>
        private List<CardsManager.Card> usedCards = new List<CardsManager.Card>();

        void Start()
        {
            // 山札の初期化
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            deckCards.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    deckCards.Add(new CardsManager.Card
                    {
                        suit = (CardsManager.Suit)i,
                        rank = (CardsManager.Rank)j
                    });
                }
            }
            Debug.Log("山札を初期化しました。カード枚数: " + deckCards.Count);
        }

        /// <summary>
        /// 山札のシャッフルを行う
        /// </summary>
        public void Shuffle()
        {
            for (int i = deckCards.Count -1; i > 0; i--)
            {
                int r = Random.Range(0, i + 1);
                var temp = deckCards[i];
                deckCards[i] = deckCards[r];
                deckCards[r] = temp;
            }

            Debug.Log("シャッフル後の山札:");
            foreach (var card in deckCards)
            {
                Debug.Log(card.suit + " " + card.rank);
            }
        }

        /// <summary>
        /// 山札からカードを一枚引く
        /// </summary>
        /// <returns></returns>
        public CardsManager.Card DrawCard()
        {
            if(deckCards.Count == 0)
            {
                // 山札が空の場合、使用済みカードを山札に戻してシャッフル
                Shuffle();
            }

            CardsManager.Card drawCard = deckCards[0];
            deckCards.RemoveAt(0);
            usedCards.Add(drawCard);

            Debug.Log(drawCard.suit + " " + drawCard.rank + "を引きました");
            return drawCard;
        }
    }
}