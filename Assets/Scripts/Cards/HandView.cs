using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
        private Transform deckTransform;    // デッキのTransform
        [SerializeField]
        private float cardSpacing = 1.5f;   //カードの間隔
        [SerializeField]
        private float moveDuration = 0.3f;  // カードが移動にかかる時間(秒)

        private List<CardsView> spawnedViews = new List<CardsView>();

        public CardsView AddCard(CardsManager.Card card, bool faceUp = true)
        {
            GameObject obj = Instantiate(cardViewPrefab, cardParent);
            CardsView view = obj.GetComponent<CardsView>();

            view.Setup(card, cardsSprite);
            view.SetSorting("Cards", 100 + spawnedViews.Count);
            view.SetFaceUp(faceUp);

            // カードのスケールを調整
            obj.transform.localScale = new Vector3(2f, 2f, 2f);

            // カードの位置を調整
            int index = spawnedViews.Count;
            Vector3 targetLocalPotision = new Vector3(index * cardSpacing, 0f, 0f);

            if(deckTransform != null)
            {
                obj.transform.position = deckTransform.position;
            }
            else
            {
                // デッキが未設定の場合はアニメーションなしで目標位置へ
                obj.transform.localPosition = targetLocalPotision;
            }

            // アニメーション開始
            StartCoroutine(AnimateCardMove(obj.transform,targetLocalPotision,moveDuration));

            spawnedViews.Add(view);
            return view;
        }

        /// <summary>
        /// カードを引いてから所定の位置まで移動させるコルーチン
        /// </summary>
        /// <param name="cardTransform"></param>
        /// <param name="targetLocalPotision"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private IEnumerator AnimateCardMove(Transform cardTransform,Vector3 targetLocalPotision,float duration)
        {
            Vector3 startLocalPos = cardTransform.localPosition;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);

                // 動き
                float easedT = t * (2f - t);

                if(cardTransform != null)
                {
                    cardTransform.localPosition = Vector3.Lerp(startLocalPos,targetLocalPotision, t);
                }

                yield return null;
            }

            if(cardTransform != null)
            {
                cardTransform.localPosition = targetLocalPotision;
            }
        }

        /// <summary>
        /// 札のカードを全て削除
        /// </summary>
        public void ClearHand()
        {
            StopAllCoroutines();

            foreach (var view in spawnedViews)
            {
                Destroy(view.gameObject);
            }
            spawnedViews.Clear();
        }

        /// <summary>
        /// カードの裏面を設定する
        /// </summary>
        /// <param name="index"></param>
        /// <param name="faceUp"></param>

        public void SetCardFaceUp(int index, bool faceUp = true)
        {
            if(index >= 0 && index < spawnedViews.Count)
            {
                spawnedViews[index].SetFaceUp(faceUp);
            }
        }
    }
}