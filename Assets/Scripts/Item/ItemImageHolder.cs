using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    /// <summary>
    /// <para>インスペクターでアイテムの画像を定義できる</para>
    /// </summary>
    [Serializable]
    public class ItemImageHolder
    {
        /// <summary>
        /// <para>空のItemImageHolder</para>
        /// </summary>
        public static readonly ItemImageHolder EMPTY = new(null, null, null);

        [Header("アイテムの名前")]
        [SerializeField] private string name = "Unknown";

        [Header("アイテムのレア度")]
        [SerializeField] private float rarity = 1.0F;

        [Header("アイテムの画像")]
        [SerializeField] private Image itemImage;

        [Header("アイテムの説明の画像")]
        [SerializeField] private Image descriptionImage;

        public string Name
        {
            get { return this.name; }
        }

        public float Rarity
        {
            get { return this.rarity; }
        }

        public Image ItemImage
        {
            get { return this.itemImage; }
        }

        public Image DescriptionImage
        {
            get { return this.descriptionImage; }
        }

        private ItemImageHolder(string name, Image itemImage, Image descriptionImage)
        {
            this.name = name;
            this.itemImage = itemImage;
            this.descriptionImage = descriptionImage;
        }

        public static String GetID(ItemImageHolder itemImageHolder)
        {
            return itemImageHolder != null ? ItemImageHolder.GetID(itemImageHolder.Name, itemImageHolder.Rarity) : ItemImageHolder.GetID("Unknown", 0.0F);
        }

        public static String GetID(string name, float rarity)
        {
            return $"{name ?? "Unknown"}:{rarity:0:00}";
        }
    }
}
