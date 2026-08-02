using System;
using UnityEngine;

namespace Item
{
    public class ItemImageRegistryBehaviour : MonoBehaviour
    {
        [Header("アイテムの画像")]
        [SerializeField] private ItemImageHolder[] itemImageHolders;

        /// <summary>
        /// <para>登録されたすべてのItemImageHolder</para>
        /// </summary>
        public ItemImageHolder[] ALL
        {
            get
            {
                ItemImageHolder[] copy = new ItemImageHolder[this.itemImageHolders.Length];

                Array.Copy(this.itemImageHolders, copy, this.itemImageHolders.Length);

                return copy;
            }
        }

        /// <summary>
        /// <para>登録されたすべてのItemImageHolderをGameManagerに送信する</para>
        /// </summary>
        public void Awake()
        {
            if (this.itemImageHolders != null && this.itemImageHolders.Length > 0)
            {
                GameManager.INSTANCE.RegisterItemImageHolders(this.itemImageHolders);
            }
        }
    }
}