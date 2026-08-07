using TMPro;
using UnityEngine;

namespace Player
{

    public class PayoutMultiplierView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text multiplierText;

        private PlayerData playerData;

        public void Setup(PlayerData data)
        {
            playerData = data;

            playerData.PayoutMultiplier.OnMultiplierChanged += UpdateMultiplierText;

            UpdateMultiplierText(playerData.PayoutMultiplier.Calculate());
        }

        private void UpdateMultiplierText(float multiplier)
        {
            if (multiplierText != null)
            {
                multiplierText.text = $"x{multiplier:F1}";
            }
        }

        private void OnDestroy()
        {
            if (playerData != null && playerData.PayoutMultiplier != null)
            {
                playerData.PayoutMultiplier.OnMultiplierChanged -= UpdateMultiplierText;
            }
        }
    }
}
