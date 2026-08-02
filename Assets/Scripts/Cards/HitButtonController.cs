using UnityEngine;
using System;

namespace Cards
{
    public class HitButtonController : MonoBehaviour
    {
        public void OnClick()
        {
            var phase = global::System.GameManager.INSTANCE.GetPhase<global::System.BlackjackPhase>("blackjack");
            phase?.TryHit();
        }
    }
}