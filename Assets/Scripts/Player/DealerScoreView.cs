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
        [SerializeField]
        private GameObject dealerScoreText; // 自分自身/テキストをアタッチ
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

        /// <summary>
        /// スコアの見た目の表示/非表示を切り替える
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActiveText(bool isActive)
        {
            dealerScoreText.SetActive(isActive);
        }
    }
}