using Player;
using UnityEngine;

namespace Item
{
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

        public void DoUse(PlayerData playerData, DealerData dealerData, float rarity)
        {
        }

        public int ComputeValue(float rarity)
        {
            return Mathf.RoundToInt(this.Value * rarity);
        }
    }
}