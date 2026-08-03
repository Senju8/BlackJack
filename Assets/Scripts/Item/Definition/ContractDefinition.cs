using Player;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// アイテム:契約書
    /// </summary>
    public class ContractDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Contract"; }
        }

        public int Value
        {
            get { return 1000; }
        }

        /// <summary>
        /// 負けたとき、ベット額が返ってくる
        /// </summary>
        /// <param name="playerData"></param>
        /// <param name="dealerData"></param>
        /// <param name="rarity"></param>
        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
            // レアリティに応じてフラグをオンにする
            switch (rarity)
            {
                case 4:

                    break;

                case 5:

                    break;
            }
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}
