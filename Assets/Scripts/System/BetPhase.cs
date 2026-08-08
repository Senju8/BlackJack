using Assets.Scripts.System;
using Bet;
using UnityEngine;
using Util;

namespace System
{
    /// <summary>
    /// <para>ベットのフェーズを定義する</para>
    /// </summary>
    public class BetPhase : GamePhase
    {
        private GameObject canvasObject;

        private GameObject betObject;
        private GameObject betDisplayObject;

        public BetPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.BetCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.BetCanvas);
            
            this.betObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.BetOnlyUIs);
            this.betDisplayObject = UIUtil.GetChild(this.canvasObject, "Bet Display");

            // ベットUIをセットする
            if (this.betObject != null && this.betDisplayObject != null && this.betObject.transform is RectTransform rectTransform)
            {
                // 位置の調整をする
                rectTransform.SetParent(this.betDisplayObject.transform);
                rectTransform.anchoredPosition = Vector2.zero;

                UIUtil.InvokeIfPresent<BetButtoms>(this.betObject, betButtoms =>
                {
                    // ベットを確定したときにショップフェーズへ遷移する
                    betButtoms.OnBetConfirmed += betAmount =>
                    {
                        this.gameManager.Call("shop");
                    };
                });

                this.betObject.SetActive(true);
            }
            
            this.canvasObject.SetActive(false);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;

            this.canvasObject.SetActive(true);
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
            if (this.canvasObject == null)
                return;

            this.canvasObject.SetActive(false);
        }

        protected override void Destroy()
        {
            if (this.canvasObject == null)
                return;

            UnityEngine.Object.Destroy(this.canvasObject);
        }

        public override void Invoke(GameObject gameObject)
        {
            if (gameObject == null)
                return;
        }
    }
}