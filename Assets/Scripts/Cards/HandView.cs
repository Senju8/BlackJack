using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// プレイヤ/ディーラーの札の見た目/動きを管理するクラス
    /// </summary>
    public class HandView : MonoBehaviour
    {
        [SerializeField]
        private GameObject cardViewPrefab;  // CardsViewを付けたPrefab
        [SerializeField]
        private CardsSprite cardsSprite;    // カードのスプライト(見た目)とカードの種類を紐づけるクラス
        [SerializeField]
        private Transform cardParent;   //カードを並べる場所の親オブジェクト
        [SerializeField]
        private float cardSpacing = 0.5f;   //カードの間隔

        private List<CardsView> spawnedViews = new List<CardsView>();

        public CardsView AddCard(CardsManager.Card card, bool faceUp = true)
        {
            GameObject obj = Instantiate(cardViewPrefab, cardParent);
            CardsView view = obj.GetComponent<CardsView>();

            view.Setup(card, cardsSprite);
            view.SetFaceUp(faceUp);

            // カードの位置を調整
            int index = spawnedViews.Count;
            obj.transform.localPosition = new Vector3(index * cardSpacing, 0f, -index * 0.01f);

            spawnedViews.Add(view);
            return view;
        }

        /// <summary>
        /// 札のカードを全て削除
        /// </summary>
        public void ClearHand()
        {
            foreach (var view in spawnedViews)
            {
                Destroy(view.gameObject);
            }
            spawnedViews.Clear();
        }
    }
}