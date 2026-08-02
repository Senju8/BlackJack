using Player;
using UnityEngine;

namespace Item
{
    public class TipDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Tip"; }
        }

        public int Value
        {
            get { return 5000; }
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