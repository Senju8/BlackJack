using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// カードのスプライトを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "CardsSprite", menuName = "Cards/CardsSprite")]
    public class CardsSprite : ScriptableObject
    {
        [System.Serializable]
        public struct CardSprite
        {
            public CardsManager.Suit suit;
            public CardsManager.Rank rank;
            public Sprite sprite;
        }

        public CardSprite[] cardSprites;
        public Sprite cardBackSprite;   // カードの裏面のスプライト

        private Dictionary<(CardsManager.Suit, CardsManager.Rank), Sprite> lookup;

        public Sprite GetSprite(CardsManager.Card card)
        {
            if(lookup == null)
            {
                lookup = new Dictionary<(CardsManager.Suit, CardsManager.Rank), Sprite>();
                foreach(var cs in cardSprites)
                {
                    lookup[(cs.suit, cs.rank)] = cs.sprite;
                }
            }

            return lookup[(card.suit, card.rank)];
        }
    }
}