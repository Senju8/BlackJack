using UnityEngine;

namespace Cards
{
    /// <summary>
    /// カードの見た目/データを合わせた表示を行うクラス
    /// </summary>
    public class CardsView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private CardsSprite cardsSprite;

        private CardsManager.Card cardData;

        /// <summary>
        /// カードの裏表
        /// </summary>
        private bool faceUp = true;

        public void Setup(CardsManager.Card card,CardsSprite cardsSprite)
        {
            cardData = card;
            this.cardsSprite = cardsSprite;
            UpdateVisual();
        }

        public void SetFaceUp(bool isFaceUp)
        {
            faceUp = isFaceUp;
            UpdateVisual();
        }

        /// <summary>
        /// カードの見た目を更新する
        /// 裏表の状態に応じてスプライトを切り替える
        /// </summary>
        private void UpdateVisual()
        {
            spriteRenderer.sprite = faceUp
                ? cardsSprite.GetSprite(cardData)
                : cardsSprite.cardBackSprite;
        }

        public void SetSorting(string layerName,int order)
        {
            spriteRenderer.sortingLayerName = layerName;
            spriteRenderer.sortingOrder = order;
        }
    }
}