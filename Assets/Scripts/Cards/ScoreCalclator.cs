using System.Collections.Generic;

namespace Cards
{
    /// <summary>
    /// ブラックジャックの計算を行うクラス
    /// </summary>
    public static class ScoreCalclator
    {
        public static int CalculateScore(List<CardsManager.Card> cards)
        {
            int score = 0;
            int aceCount = 0;

            foreach(var card in cards)
            {
                int value = GetCardsValue(card.rank);
                score += value;

                if(card.rank == CardsManager.Rank.Ace)
                {
                    aceCount++;
                }
            }

            // Aceを11として扱っても21を超えない場合は+10する
            while(aceCount>0 && score+10 <= 21)
            {
                score+=10;
                aceCount--;
            }

            return score;
        }

        /// <summary>
        /// カードごとのスコアを整数値で取得
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        private static int GetCardsValue(CardsManager.Rank rank)
        {
            // ランクがJack,Queen,Kingなら10で返す
            if(rank >= CardsManager.Rank.Jack)
            {
                return 10;
            }

            return (int)rank;
        }

        /// <summary>
        /// バーストしているかどうか
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsBurst(List<CardsManager.Card> cards)
        {
            return CalculateScore(cards) > 21;
        }

        /// <summary>
        /// ブラックジャックかどうか
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsBackjack(List<CardsManager.Card> cards)
        {
            return cards.Count == 2 && CalculateScore(cards) == 21;
        }
    }
}