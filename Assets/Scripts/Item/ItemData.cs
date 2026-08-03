using Player;
using System;
using System.Threading;
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
        /// <para>コモン: 1.0</para>
        /// <para>アンコモン: 2.0</para>
        /// <para>レア: 3.0</para>
        /// <para>エピック: 4.0</para>
        /// <para>レジェンド: 5.0</para>
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
            set { this.value = Mathf.Max(0, value); }
        }

        /// <summary>
        /// <para>アイテムの個数</para>
        /// </summary>
        public int Count
        {
            get { return this.count; }
            set { this.count = Mathf.Max(0, value); }
        }

        /// <summary>
        /// <para>アイテムの値段</para>
        /// </summary>
        public Sprite Sprite
        {
            get { return this.sprite; }
            set { this.sprite = value; }
        }

        public ItemData(ItemDefinition itemDefinition, float rarity = 1.0F, int count = 0)
        {
            this.itemDefinition = itemDefinition;
            this.Rarity = rarity;
            this.Count = count;
        }

        /// <summary>
        /// <para>ItemDataのクローンを返す</para>
        /// </summary>
        public ItemData Clone()
        {
            ItemData itemData = new ItemData(this.itemDefinition, this.rarity, this.count);

            itemData.Value = this.Value;

            return itemData;
        }

        public override bool Equals(object target)
        {
            if (target == this)
            {
                return true;
            }
            else if (target is ItemData itemData)
            {
                return itemData.Name == this.Name && itemData.rarity == this.rarity;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Rarity);
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
        public virtual void DoUse(PlayerData playerData, DealerData dealerData, int count = 1)
        {
            if (this.itemDefinition == null)
                return;


            this.itemDefinition.DoUse(playerData, dealerData, this.rarity);

            --this.Count;
        }
    }
}