using Player;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// <para>プレイヤーの使用するアイテムの実体</para>
    /// </summary>
    public class ItemData
    {
        public static readonly ItemData EMPTY = new(null);

        private readonly ItemDefinition itemDefinition;

        private float rarity = 1.0F;
        private int value = 0;
        private int count = 0;

        private Sprite sprite;

        /// <summary>
        /// <para>アイテムの定義</para>
        /// </summary>
        public ItemDefinition ItemDefinition
        {
            get { return this.itemDefinition; }
        }

        /// <summary>
        /// <para>アイテムの名前</para>
        /// </summary>
        public string Name
        {
            get { return this.itemDefinition != null ? this.itemDefinition.Name : "Unknown"; }
        }

        /// <summary>
        /// <para>アイテムのレア度</para>
        /// </summary>
        public float Rarity
        {
            get { return this.rarity; }
            set { this.rarity = Mathf.Max(1.0F, value); }
        }

        /// <summary>
        /// <para>アイテムの値段</para>
        /// </summary>
        public int Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// <para>アイテムの個数</para>
        /// </summary>
        public int Count
        {
            get { return this.count; }
            set { this.count = Mathf.Max(0, value); }
        }

        public Sprite Sprite
        {
            get { return this.sprite; }
            set { this.sprite = value; }
        }

        public ItemData(ItemDefinition itemDefinition, float rarity = 1.0F, int count = 0)
        {
            this.itemDefinition = itemDefinition;
            this.Rarity = rarity;
            this.value = itemDefinition != null ? itemDefinition.Value : 0;
            this.Count = count;
        }

        /// <summary>
        /// <para>アイテムを使用できるかどうか</para>
        /// </summary>
        public virtual bool CanUse(PlayerData playerData, DealerData dealerData)
        {
            if (this.itemDefinition == null || !this.itemDefinition.CanUse(playerData, dealerData, this.rarity))
                return false;

            if (this.count <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// <para>アイテムを使用する</para>
        /// </summary>
        public virtual void DoUse(PlayerData playerData, DealerData dealerData)
        {
            if (this.itemDefinition == null)
                return;

            this.itemDefinition.DoUse(playerData, dealerData, this.rarity);

            --this.Count;
        }
    }
}