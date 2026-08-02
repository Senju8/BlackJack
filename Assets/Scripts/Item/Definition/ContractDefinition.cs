using Player;
using UnityEngine;

namespace Item
{
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

        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}
