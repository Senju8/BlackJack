using Cards;
using Player;

namespace Item
{
    /// <summary>
    /// <para>プレイヤーが使用するアイテムを定義する</para>
    /// </summary>
    public interface ItemDefinition
    {
        public string Name { get;  }

        /// <summary>
        /// <para>アイテムの値段</para>
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// <para>アイテムを使用できるかどうか</para>
        /// </summary>
        public bool CanUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity) { return true; }

        /// <summary>
        /// <para>アイテムを使用する</para>
        /// </summary>
        public void DoUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity);

        /// <summary>
        /// <para>レア度に応じた値段を計算する</para>
        /// </summary>
        public int ComputeValue(float rarity)
        {
            return this.Value;
        }
    }
}
