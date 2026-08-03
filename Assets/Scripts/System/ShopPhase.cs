using Assets.Scripts.System;
using Item;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        private GameObject itemName;
        private GameObject itemDescription;
        private GameObject noItemDescription;
        private GameObject itemTotalValue;
        private GameObject playerMoney;
        private GameObject buyObject;

        private List<GameObject> itemDIsplaySlots;
        private List<GameObject> itemCartSlots;

        private Dictionary<GameObject, ItemData> itemDisplayData;
        private Dictionary<GameObject, ItemData> itemCartData;

        private int itemTotalValueBuffer;
        private int playerMoneyBuffer;

        public ShopPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.ShopCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ShopCanvas);

            // 子GameObjectの取得
            this.itemDisplayObject = UIUtil.GetChild(this.canvasObject, "Item Display");
            this.itemCartObject = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Item Cart View/Item Cart");
            this.itemName = UIUtil.GetChild(this.canvasObject, "Item Description/Name Display/Name");
            this.itemDescription = UIUtil.GetChild(this.canvasObject, "Item Description/Description Display/Description");
            this.noItemDescription = UIUtil.GetChild(this.canvasObject, "Item Description/Description Display/No Description");
            this.itemTotalValue = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Item Total Value View/Item Total Value Display/Item Total Value");
            this.playerMoney = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Player Money View/Player Money Display/Player Money");
            this.buyObject = UIUtil.GetChild(this.canvasObject, "Item Cart Display/Buy Display/Buy");

            this.canvasObject.SetActive(false);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;
            if (this.itemDisplayObject == null)
                return;
            if (this.itemCartObject == null)
                return;

            int itemCount = 6;

            // アイテムスロットを初期化
            this.itemDIsplaySlots = new List<GameObject>(itemCount);
            this.itemDisplayData = new Dictionary<GameObject, ItemData>(itemCount);

            ItemDefinition itemDefinition;
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
                    itemDefinition = new DevilcallDefinition();
                }
                else if (posibility < 0.04D)
                {
                    // Dice
                    itemDefinition = new DiceDefinition();
                }
                else if (posibility < 0.40D)
                {
                    // Contract
                    itemDefinition = new ContractDefinition();
                    rarity = random.NextDouble() < 0.6D ? 4 : 5;
                }
                else
                {
                    // Tip
                    itemDefinition = new TipDefinition();
                }

                // ItemDataを生成する
                itemData = new ItemData(itemDefinition, rarity, 1);

                // レア度から値段を設定する
                itemData.Value = itemDefinition.ComputeValue(rarity);

                // アイテムスロットに設定する
                if (this.gameManagerBehaviour.ItemDisplaySlot != null)
                {
                    // アイテムスロットを生成する
                    itemDisplaySlotObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ItemDisplaySlot);

                    if (itemDisplaySlotObject != null)
                    {
                        GameObject item = UIUtil.GetChild(itemDisplaySlotObject, "Display/Item");
                        GameObject value = UIUtil.GetChild(itemDisplaySlotObject, "Information/Value");

                        // アイテムの画像と値段を設定する
                        if (item != null && value != null)
                        {
                            Image image = item.GetComponent<Image>();
                            TextMeshProUGUI textMeshProUGUI = value.GetComponent<TextMeshProUGUI>();

                            if (image != null && value != null)
                            {
                                ItemImageHolder itemImageHolder = this.gameManager.GetItemImageHolder(itemData.Name, itemData.Rarity);

                                image.sprite = itemImageHolder.ItemImage?.sprite;
                                image.color = itemImageHolder.ItemImage != null ? itemImageHolder.ItemImage.color : new Color(0.0F, 0.0F, 0.0F, 0.0F);
                                textMeshProUGUI.text = itemData.Value.ToString("N0");
                            }
                        }

                        // アイテムスロットUIとして追加する
                        itemDisplaySlotObject.transform.SetParent(this.itemDisplayObject.transform, false);

                        // アイテムスロットとアイテムデータを紐づけする
                        this.itemDisplayData[itemDisplaySlotObject] = itemData;

                        // アイテムスロットを登録する
                        this.itemDIsplaySlots.Add(itemDisplaySlotObject);
                    }
                }
            }

            // カートスロットを初期化
            this.itemCartSlots = new List<GameObject>();
            this.itemCartData = new Dictionary<GameObject, ItemData>();

            this.SetItemDescription(null);
            this.canvasObject.SetActive(true);
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
            if (this.canvasObject == null)
                return;
            if (this.itemDisplayObject == null)
                return;
            if (this.itemCartObject == null)
                return;

            // アイテムスロットをクリア
            foreach (GameObject gameObject in this.itemDIsplaySlots)
            {
                if (gameObject != null)
                    UnityEngine.Object.Destroy(gameObject);
            }

            // カートスロットをクリア
            foreach (GameObject gameObject in this.itemCartSlots)
            {
                if (gameObject != null)
                    UnityEngine.Object.Destroy(gameObject);
            }

            this.canvasObject.SetActive(false);
        }

        protected override void Destroy()
        {
            if (this.canvasObject != null)
                UnityEngine.Object.Destroy(this.canvasObject);
        }

        public override void Invoke(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            // Shop Canvas UIの更新をする
            this.ClickShopCanvasUI(gameObject);
            this.UpdatePlayerMoney();
            this.UpdateItemTotalValue();

            switch (gameObject.name)
            {
                case "Buy":
                    if (this.gameManager.InfiniteMoneyMode || this.itemTotalValueBuffer < this.playerMoneyBuffer)
                    {
                        // プレイヤーにアイテムを追加
                        this.gameManager.AddPlayerItemData(this.itemCartData.Values.ToArray());

                        // プレイヤーの所持金を減らす
                        this.gameManager.playerData.SetValues(Mathf.Max(0, this.playerMoneyBuffer - this.itemTotalValueBuffer));

                        // 次のGamePhaseを呼び出す
                        this.gameManager.Call("blackjack");

                        // サウンドを再生
                        this.gameManager.Play("購入");
                    }

                    return;
            }
        }

        public override void Invoke(GameObject gameObject, params object[] contexts)
        {
            // アイテムスロットにカーソルが乗ったかどうか検知する
            if (gameObject != null && contexts != null && contexts.Length >= 1 && contexts[0] is string type && type == "Pointer Enter")
            {
                Transform parent = gameObject.transform.parent;

                if (parent != null)
                {
                    this.SetItemDescription(parent.gameObject);
                }
            }
        }

        private void ClickShopCanvasUI(GameObject gameObject)
        {
            // アイテムスロットが押されたかどうか検知する
            GameObject parent = gameObject.transform.parent?.gameObject;

            if (parent != null)
            {
                foreach (GameObject itemDisplaySlot in this.itemDIsplaySlots)
                {
                    if (itemDisplaySlot == parent)
                    {
                        this.ClickItemDisplaySlot(itemDisplaySlot);

                        return;
                    }
                }
            }

            // カートスロットが押されたかどうか検知する
            parent = gameObject.transform.parent?.gameObject.transform.parent?.gameObject.transform.parent?.gameObject;

            if (parent != null)
            {
                foreach (GameObject itemCartSlotObject in this.itemCartSlots)
                {
                    if (itemCartSlotObject != null && itemCartSlotObject.GetInstanceID() == parent.GetInstanceID())
                    {
                        this.ClickItemCartRemove(itemCartSlotObject);

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// <para>クリックされたアイテムをカートスロットに追加する</para>
        /// </summary>
        private void ClickItemDisplaySlot(GameObject itemDisplaySlotObject)
        {
            if (itemDisplaySlotObject == null)
                return;

            if (this.itemDisplayData.ContainsKey(itemDisplaySlotObject))
            {
                ItemData itemData = this.itemDisplayData[itemDisplaySlotObject]?.Clone();

                // ItemDataがすでにカートスロットに存在するか確認する
                bool hasItemData = itemData != null && this.itemCartData.ContainsValue(itemData);

                GameObject itemCartSlotObject = null;

                if (itemData != null)
                {
                    if (hasItemData)
                    {
                        foreach (var (key, value) in this.itemCartData)
                        {
                            if (value != null && itemData.Equals(value))
                            {
                                itemCartSlotObject = key;

                                // ItemDataの個数を増やす
                                value.Count += itemData.Count;
                                itemData = value;

                                break;
                            }
                        }
                    }
                    else
                    {
                        // カートスロットを生成する
                        itemCartSlotObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ItemCartSlot);
                    }
                }

                if (itemCartSlotObject != null)
                {
                    // 個数、名前、値段を設定する
                    GameObject count = UIUtil.GetChild(itemCartSlotObject, "Item Cart Display/Count");
                    GameObject name = UIUtil.GetChild(itemCartSlotObject, "Item Cart Display/Name");
                    GameObject value = UIUtil.GetChild(itemCartSlotObject, "Item Cart Display/Value");

                    if (count != null && name != null && value != null)
                    {
                        TextMeshProUGUI countTextMeshProUGUI = count.GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI nameTextMeshProUGUI = name.GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI valueTextMeshProUGUI = value.GetComponent<TextMeshProUGUI>();

                        if (countTextMeshProUGUI != null && nameTextMeshProUGUI != null && valueTextMeshProUGUI != null)
                        {
                            countTextMeshProUGUI.text = $"x{itemData.Count}";
                            nameTextMeshProUGUI.text = itemData.Name;
                            valueTextMeshProUGUI.text = $"{itemData.Value:N0}$";
                        }
                    }

                    if (!hasItemData)
                    {
                        // カートスロットUIとして追加する
                        itemCartSlotObject.transform.SetParent(this.itemCartObject.transform, false);

                        // カートスロットとアイテムデータを紐づけする
                        this.itemCartData[itemCartSlotObject] = itemData;

                        // カートスロットを登録する
                        this.itemCartSlots.Add(itemCartSlotObject);
                    }
                }
            }
        }

        /// <summary>
        /// <para>カートスロットのアイテムを1コ減らす</para>
        /// </summary>
        private void ClickItemCartRemove(GameObject itemCartSlotObject)
        {
            if (itemCartSlotObject == null)
                return;

            // ItemDataの個数を減らす
            ItemData itemData = this.itemCartData.ContainsKey(itemCartSlotObject) ? this.itemCartData[itemCartSlotObject] : null;
            GameObject count = UIUtil.GetChild(itemCartSlotObject, "Item Cart Display/Count");

            if (itemData != null && count != null)
            {
                --itemData.Count;

                if (itemData.Count > 0)
                {
                    // カートスロットUIに反映する
                    TextMeshProUGUI countTextMeshProUGUI = count.GetComponent<TextMeshProUGUI>();

                    if (countTextMeshProUGUI != null)
                    {
                        countTextMeshProUGUI.text = $"x{itemData.Count}";
                    }
                }
                else
                {
                    // カートスロットを破棄する
                    this.itemCartSlots.Remove(itemCartSlotObject);

                    this.itemCartData.Remove(itemCartSlotObject);

                    UnityEngine.Object.Destroy(itemCartSlotObject);
                }
            }
        }

        /// <summary>
        /// <para>アイテムの説明を設定する</para>
        /// </summary>
        private void SetItemDescription(GameObject itemDisplaySlotObject)
        {
            if (this.itemName == null || this.itemDescription == null)
                return;

            TextMeshProUGUI textMeshProUGUI = this.itemName.GetComponent<TextMeshProUGUI>();
            Image image = this.itemDescription.GetComponent<Image>();

            if (textMeshProUGUI == null || image == null)
                return;
            
            // アイテムの説明を空にする
            if (itemDisplaySlotObject == null)
            {
                textMeshProUGUI.text = "";
                image.sprite = null;
                image.color = new Color(0.0F, 0.0F, 0.0F, 0.0F);

                this.noItemDescription.SetActive(true);

                return;
            }

            // アイテムの説明を設定する
            if (this.itemDisplayData.ContainsKey(itemDisplaySlotObject))
            {
                ItemData itemData = this.itemDisplayData[itemDisplaySlotObject];

                if (itemData == null)
                    return;

                ItemImageHolder itemImageHolder = this.gameManager.GetItemImageHolder(itemData.Name, itemData.Rarity);

                textMeshProUGUI.text = itemData.Name;
                image.sprite = itemImageHolder.DescriptionImage != null ? itemImageHolder.DescriptionImage.sprite : null;
                image.color = itemImageHolder.DescriptionImage != null ? itemImageHolder.DescriptionImage.color : new Color(0.0F, 0.0F, 0.0F, 0.0F);

                this.noItemDescription.SetActive(itemImageHolder.DescriptionImage == null);
            }
        }

        /// <summary>
        /// <para>カートスロットのアイテムの合計金額を出力する</para>
        /// </summary>
        private void UpdateItemTotalValue()
        {
            if (this.itemTotalValue == null)
                return;

            TextMeshProUGUI textMeshProUGUI = this.itemTotalValue.GetComponent<TextMeshProUGUI>();

            if (textMeshProUGUI)
            {
                this.itemTotalValueBuffer = 0;

                foreach (ItemData itemData in this.itemCartData.Values)
                {
                    if (itemData == null)
                        continue;

                    this.itemTotalValueBuffer += itemData.Value * itemData.Count;
                }

                textMeshProUGUI.text = this.itemTotalValueBuffer.ToString("N0");
                textMeshProUGUI.color = this.playerMoneyBuffer > 0 && this.itemTotalValueBuffer >= this.playerMoneyBuffer ? Color.red : Color.white;
            }
        }

        /// <summary>
        /// <para>プレイヤーの所持金を出力する</para>
        /// </summary>
        private void UpdatePlayerMoney()
        {
            if (this.playerMoney == null)
                return;

            TextMeshProUGUI textMeshProUGUI = this.playerMoney.GetComponent<TextMeshProUGUI>();

            if (textMeshProUGUI)
            {
                this.playerMoneyBuffer = this.gameManager.playerData.GetValues();

                textMeshProUGUI.text = this.playerMoneyBuffer.ToString("N0");
            }
        }
    }
}
