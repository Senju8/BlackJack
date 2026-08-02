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
        public bool CanUse(PlayerData playerData, DealerData dealerData, float rarity) { return true; }

        /// <summary>
        /// <para>アイテムを使用する</para>
        /// </summary>
        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity);

        /// <summary>
        /// <para>レア度から計算した値段を返す</para>
        /// </summary>
        public int ComputeValue(float rarity)
        {
            return this.Value;
        }
    }
}
