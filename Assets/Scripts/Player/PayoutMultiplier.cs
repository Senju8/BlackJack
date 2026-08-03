using System;
using System.Collections.Generic;

namespace Player
{
    /// <summary>
    /// 
    /// </summary>
    public class PayoutMultiplier
    {
        private readonly Dictionary<string, float> bonuses = new();

        public event Action<float> OnMultiplierChanged;

        /// <summary>
        /// 倍率ボーナスを設定する
        /// 
        /// 同じreasonで再度呼ぶと上書きされる（都度加算されて増え続けることはない）
        /// </summary>
        /// <param name="reason">効果の識別名（例: "blackjack", "item_luckyClover"）</param>
        /// <param name="value">加算する倍率（例: 0.5fで+0.5倍）</param>
        public void SetBonus(string reason, float value)
        {
            if (string.IsNullOrEmpty(reason))
            {
                return;
            }

            bonuses[reason] = value;
            OnMultiplierChanged?.Invoke(Calculate());   // イベント発火
        }

        /// <summary>
        /// 指定した効果を取り除く
        /// </summary>
        public void RemoveBonus(string reason)
        {
            if (reason == null)
            {
                return;
            }

            if(bonuses.Remove(reason))
            {
                OnMultiplierChanged?.Invoke(Calculate());   // イベント発火
            }
        }

        /// <summary>
        /// 全ての効果をクリアする
        /// 
        /// ラウンド終了時、効果を一括で削除
        /// </summary>
        public void Clear()
        {
            bonuses.Clear();
            OnMultiplierChanged?.Invoke(Calculate());   // イベント発火
        }

        /// <summary>
        /// 現在登録されている全ボーナスの合計値を返す
        /// </summary>
        public float GetBonusTotal()
        {
            float total = 0f;

            foreach (float value in bonuses.Values)
            {
                total += value;
            }

            return total;
        }

        /// <summary>
        /// 基本倍率(1.0) + 全ボーナスの最終倍率を返す
        /// </summary>
        public float Calculate()
        {
            return 1.0f + GetBonusTotal();
        }
    }
}