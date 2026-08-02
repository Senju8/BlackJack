using Player;
using System;
using TMPro;
using UnityEngine;

namespace Bet
{
    /// <summary>
    /// ベット額を指定する際のボタンの動き
    /// </summary>
    public class BetButtoms : MonoBehaviour
    {
        [SerializeField] private TMP_Text betDisplayText;

        /// <summary>
        /// 掛け金額の最大桁数
        /// </summary>
        [SerializeField] private int maxDigits = 7;

        /// <summary>
        /// 入力中の数字を溜めるバッファ
        /// </summary>
        private string inputBuffer = "";

        /// <summary>
        /// ベット確定時に発火するイベント
        /// 
        /// BlackJackPhaseで購読する
        /// </summary>
        public event Action<int> OnBetConfirmed;

        /// <summary>
        /// ベットボタン
        /// </summary>
        public void OnClickBet()
        {
            if (!TryGetInputValue(out int amount))
            {
                Debug.Log("金額が入力されていません");
                return;
            }

            PlayerData playerData = System.GameManager.INSTANCE.playerData;

            if (!playerData.TryConfirmBet(amount))
            {
                // 所持金を超えている、または0円などの不正な入力
                Debug.Log($"ベット失敗（入力額: {amount}, 所持金: {playerData.GetValues()}）");
                ResetInput();
                return;
            }

            OnBetConfirmed?.Invoke(amount);

            inputBuffer = "";
            UpdateDisplay();
        }

        /// <summary>
        /// 番号ボタン(0～9)
        /// </summary>
        /// <param name="num"></param>
        public void OnClickNum(int num)
        {
            if(num < 0 || num > 9)
            {
                return;
            }

            AppendDigits(num.ToString());
        }

        /// <summary>
        /// 00ボタン
        /// 
        /// バッファが空の状態で押しても意味が無い（0のまま）ので何もしない
        /// </summary>
        public void OnClickDoubleZero()
        {
            if (inputBuffer.Length == 0)
            {
                return;
            }

            AppendDigits("00");
        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        public void OnClickClear()
        {
            inputBuffer = "";
            UpdateDisplay();
        }

        /// <summary>
        /// 入力状隊をリセットする
        /// </summary>
        public void ResetInput()
        {
            inputBuffer = "";
            UpdateDisplay();
        }

        /// <summary>
        /// バッファへの数字追記共通処理
        /// </summary>
        private void AppendDigits(string digits)
        {
            if (inputBuffer.Length + digits.Length > maxDigits)
            {
                return;
            }

            inputBuffer += digits;
            UpdateDisplay();
        }

        /// <summary>
        /// 現在のバッファをintに変換する
        /// 
        /// 空文字、または0のみの場合はfalseを返す（ベット額として不正なため）
        /// </summary>
        private bool TryGetInputValue(out int amount)
        {
            amount = 0;

            if (string.IsNullOrEmpty(inputBuffer))
            {
                return false;
            }

            if (!int.TryParse(inputBuffer, out amount))
            {
                return false;
            }

            return amount > 0;
        }

        /// <summary>
        /// 入力中の金額を画面に反映する
        /// </summary>
        private void UpdateDisplay()
        {
            if (betDisplayText == null)
            {
                return;
            }

            string shown = string.IsNullOrEmpty(inputBuffer) ? "0" : inputBuffer;
            betDisplayText.text = shown;
        }
    }
}