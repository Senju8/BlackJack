using TMPro;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤの所持金額を表示させる
    /// </summary>
    public class PlayerValueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueScore;

        private PlayerData playerData;

        public void Setup(PlayerData data)
        {
            playerData = data;
            playerData.OnValueChanged += UpdateValueText;

            UpdateValueText(playerData.GetValues());
        }

        private void UpdateValueText(int value)
        {
            valueScore.text = "" + value + "$";
        }

        private void OnDestroy()
        {
            if(playerData != null)
            {
                playerData.OnValueChanged -= UpdateValueText;
            }
        }
    }
}