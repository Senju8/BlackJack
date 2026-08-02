using Assets.Scripts.System;
using UnityEngine;
using Util;

namespace System
{
    /// <summary>
    /// <para>アイテムの購入のフェーズを定義する</para>
    /// </summary>
    public class ShopPhase : GamePhase
    {
        private GameObject canvasObject;

        public ShopPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.BuyCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.BuyCanvas);

            //UnityEngine.Debug.Log(UIUtil.GetChild(this.canvasObject, "Item Cart Display/Buy Display/Buy"));

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
