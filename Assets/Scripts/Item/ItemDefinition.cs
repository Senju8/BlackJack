namespace Item
{
    /// <summary>
    /// <para>プレイヤーが使用するアイテムを定義する</para>
    /// </summary>
    public interface ItemDefinition
    {
        /// <summary>
        /// <para>アイテムの値段</para>
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// <para>アイテムを使用できるかどうか</para>
        /// </summary>
        public bool CanUse(float rarity) { return true; }

        /// <summary>
        /// <para>アイテムを使用する</para>
        /// </summary>
        public void DoUse(float rarity);
    }
}
