using Cards;
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
        /// ベット額を加算
        /// </summary>
        /// <param name="playerData"></param>
        /// <param name="dealerData"></param>
        /// <param name="rarity"></param>
        public void DoUse(PlayerData playerData, DealerData dealerData, Deck deck, float rarity)
        {
            switch (rarity)
            {
                case 4:
                    playerData.AddValues(playerData.GetBet() / 2);
                    break;

                case 5:
                    playerData.AddValues(playerData.GetBet());
                    break;
            }
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}
