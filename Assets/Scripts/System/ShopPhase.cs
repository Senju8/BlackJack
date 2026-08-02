using Assets.Scripts.System;
using Item;
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

        private GameObject itemDisplayObject;
        private GameObject itemCartObject;
        private GameObject buyObject;

        private GameObject[] itemDIsplaySlots;
        private GameObject[] itemCartSlots;

        private ItemData[] itemDataArray;

        public ShopPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.ShopCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ShopCanvas);

            // 子GameObjectの取得
            this.itemDisplayObject = UIUtil.GetChild(this.canvasObject, "Item Display");
            this.itemCartObject = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Item Cart");
            this.buyObject = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Buy Display/Buy");

            this.canvasObject.SetActive(false);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;

            int itemCount = 6;

            // アイテムスロットを初期化
            this.itemDIsplaySlots = new GameObject[itemCount];
            this.itemDataArray = new ItemData[itemCount];

            ItemData itemData;

            Random random = new Random();
            double posibility;
            int rarity;

            GameObject itemDisplaySlotObject;
            GameObject itemCartSlotObject;

            for (int i = 0; i < itemCount; ++i)
            {
                posibility = random.NextDouble();
                rarity = Mathf.RoundToInt((float) (1.0D + random.NextDouble() * 4.0D)); // 1 ～ 5

                if (posibility < 0.02D)
                {
                    // Devil Call
                    itemData = new ItemData(DevilcallDefinition.INSTANCE, DevilcallDefinition.INSTANCE.Value + 1000 * (rarity - 1), 1);
                }
                else if (posibility < 0.04D)
                {
                    // Dice
                    itemData = new ItemData(DiceDefinition.INSTANCE, DiceDefinition.INSTANCE.Value + 1000 * (rarity - 1), 1);
                }
                else if (posibility < 0.36D)
                {
                    // Contract
                    itemData = new ItemData(ContractDefinition.INSTANCE, ContractDefinition.INSTANCE.Value + 100000 * (rarity - 1), 1);
                }
                else
                {
                    // Tip
                    itemData = new ItemData(TipDefinition.INSTANCE, TipDefinition.INSTANCE.Value + 100000 * (rarity - 1), 1);
                }

                this.itemDataArray[i] = itemData;

                if (this.gameManagerBehaviour.ItemDisplaySlot != null)
                {
                    this.itemDIsplaySlots[i] = itemDisplaySlotObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ItemDisplaySlot);

                    if (itemDisplaySlotObject != null)
                    {
                        //GameObject item = UnityEngine.Object.Instantiate
                    }
                }
            }

            // カートスロットを初期化
            this.canvasObject.SetActive(true);
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
            if (this.canvasObject == null)
                return;

            // アイテムスロットをクリア

            // カートスロットをクリア

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

            switch (gameObject.name)
            {
                case "Buy":
                    this.gameManager.Call("blackjack");

                    break;
            }
        }
    }
}
