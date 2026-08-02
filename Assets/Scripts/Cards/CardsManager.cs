
namespace Cards
{
    /// <summary>
    /// カード全部の管理を行うクラス
    /// </summary>
    public class CardsManager
    {
        /// <summary>
        /// カードのスートを持つ列挙型
        /// </summary>
        public enum Suit
        {
            Spade,Heart,Diamond,Club
        }

        /// <summary>
        /// カードのランクを持つ列挙型
        /// </summary>
        public enum Rank
        {
            Ace = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10,
            Jack = 11, Queen = 12, King = 13
        }

        /// <summary>
        /// カードのスートとランクを持つ構造体
        /// </summary>
        [System.Serializable]
        public struct Card
        {
            public Suit suit;
            public Rank rank;
        }
    }
}