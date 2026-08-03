using Player;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// アイテム:デビルコール
    /// </summary>
    public class DevilcallDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Devil Call"; }
        }

        public int Value
        {
            get { return 100000; }
        }

        /// <summary>
        /// ディーラーの最終的な点数にいくつかプラス
        /// </summary>
        /// <param name="playerData"></param>
        /// <param name="dealerData"></param>
        /// <param name="rarity"></param>
        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
            //　レアリティに応じてフラグをtrueにする
            switch(rarity)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
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