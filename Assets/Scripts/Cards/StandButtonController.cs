using UnityEngine;

namespace Cards
{
    public class StandButtonController : MonoBehaviour
    {
        public void OnClick()
        {
            var phase = global::System.GameManager.INSTANCE.GetPhase<global::System.BlackjackPhase>("blackjack");
            phase?.TryStand();
        }
    }
}