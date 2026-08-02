using Player;
using UnityEngine;

namespace Item
{
    public class DiceDefinition : ItemDefinition
    {
        public string Name
        {
            get { return "Dice"; }
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