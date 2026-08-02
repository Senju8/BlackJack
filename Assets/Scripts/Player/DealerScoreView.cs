using TMPro;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤのスコアの見た目処理
    /// </summary>
    public class DealerScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;
        private DealerData dealerData;

        public void Setup(DealerData data)
        {
            dealerData = data;
            dealerData.OnScoreChanded += UpdateScoreText;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = ""+ score;
        }

        private void OnDestroy()
        {
            if(dealerData != null)
            {
                dealerData.OnScoreChanded -= UpdateScoreText;
            }
        }
    }
}