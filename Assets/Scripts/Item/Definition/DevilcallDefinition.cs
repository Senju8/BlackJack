using Cards;
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
        public void DoUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity)
        {
            //　レアリティに応じてフラグをtrueにする
            switch(rarity)
            {
                case 1:
                    dealerData.AddScore(1);
                    break;
                case 2:
                    dealerData.AddScore(2);
                    break;
                case 3:
                    dealerData.AddScore(3);
                    break;
                case 4:
                    dealerData.AddScore(4);
                    break;
                case 5:
                    dealerData.AddScore(5);
                    break;
            }
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}