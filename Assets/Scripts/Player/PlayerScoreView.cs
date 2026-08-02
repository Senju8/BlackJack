using TMPro;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤのスコアの見た目処理
    /// </summary>
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;
        private PlayerData playerData;

        public void Setup(PlayerData data)
        {
            playerData = data;
            playerData.OnScoreChanded += UpdateScoreText;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = "Score" + score;
        }

        private void OnDestroy()
        {
            if(playerData != null)
            {
                playerData.OnScoreChanded -= UpdateScoreText;
            }
        }
    }
}