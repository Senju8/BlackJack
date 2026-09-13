using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>UIに表示されるテキストを保持する</para>
    /// </summary>
    public class T
    {
        public static readonly TextTable INSTANCE = new();

        /// <summary>
        /// <para>使用するすべてのテキストを登録する</para>
        /// </summary>
        public static void Initialize()
        {
            // 英語
            Bind(TextTable.Lang.EN);

            Set("start.title", "BLACKJACK");
            Set("start.start", "Start");
            Set("start.exit", "Exit");

            Set("difficulty.select", "Select a difficulty");
            Set("difficulty.money", "Your money");
            Set("difficulty.quota", "Quota amount");
            Set("difficulty.easy", "EASY");
            Set("difficulty.normal", "NORMAL");
            Set("difficulty.hard", "HARD");
            Set("difficulty.ok", "OK");

            Set("result.none", "No results...");
            Set("result.win", "You win!");
            Set("result.draw", "It's a draw...");
            Set("result.lose", "You lose...");
            Set("result.next", "Next");
            Set("result.finish", "Finish");
            Set("result.exit", "Exit");
            Set("result.difficulty", "Difficulty");
            Set("result.quota", "Quota amount");
            Set("result.bet", "Bet");
            Set("result.money", "Your money");
            Set("result.score", "Score");

            // 日本語
            Bind(TextTable.Lang.JP);

            Set("start.title", "BLACKJACK");
            Set("start.start", "ゲームをスタート");
            Set("start.exit", "ゲームを終了する");

            Set("difficulty.select", "難易度を選択してください");
            Set("difficulty.money", "所持金");
            Set("difficulty.quota", "ノルマ金額");
            Set("difficulty.easy", "イージー");
            Set("difficulty.normal", "ノーマル");
            Set("difficulty.hard", "ハード");
            Set("difficulty.ok", "OK");

            Set("result.none", "リザルトが設定されていません…");
            Set("result.win", "勝利！");
            Set("result.draw", "引き分け…");
            Set("result.lose", "敗北…");
            Set("result.next", "次に進む");
            Set("result.finish", "タイトルに戻る");
            Set("result.exit", "ゲームを終了する");
            Set("result.difficulty", "難易度");
            Set("result.quota", "ノルマ金額");
            Set("result.bet", "ベット");
            Set("result.money", "所持金");
            Set("result.score", "スコア");
        }

        public static void Bind(TextTable.Lang lang)
        {
            INSTANCE.Bind(lang);
        }

        public static void Set(string id, string texts)
        {
            INSTANCE.Set(id, texts);
        }

        public static string Get(string id)
        {
            return INSTANCE.Get(id);
        }
    }
}
