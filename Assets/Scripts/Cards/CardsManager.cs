
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
            Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
            Jack, Queen, King
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