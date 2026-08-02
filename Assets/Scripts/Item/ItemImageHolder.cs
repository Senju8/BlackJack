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
        public static readonly ItemImageHolder EMPTY = new(null, null, Color.white, null, Color.white);

        [Header("アイテムの名前")]
        [SerializeField] private string name = "Unknown";

        [Header("アイテムの画像")]
        [SerializeField] private Image itemImage;

        [Header("アイテムのカラー")]
        [SerializeField] private Color itemColor;

        [Header("アイテムの説明の画像")]
        [SerializeField] private Image descriptionImage;

        [Header("アイテムの説明のカラー")]
        [SerializeField] private Color descriptionColor;

        public string Name
        {
            get { return this.name; }
        }

        public Image ItemImage
        {
            get { return this.itemImage; }
        }

        public Color ItemColor
        {
            get { return this.itemColor; }
        }

        public Image DescriptionImage
        {
            get { return this.descriptionImage; }
        }

        public Color DescriptionColor
        {
            get { return this.descriptionColor; }
        }

        private ItemImageHolder(string name, Image itemImage, Color itemColor, Image descriptionImage, Color descriptionColor)
        {
            this.name = name;
            this.itemImage = itemImage;
            this.itemColor = itemColor;
            this.descriptionImage = descriptionImage;
            this.descriptionColor = descriptionColor;
        }
    }
}
