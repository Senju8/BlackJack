using System;
using UnityEngine;

namespace GameDebug
{
    public class GameActionBehaviour : MonoBehaviour
    {
        public void Invoke()
        {
            GameManager.INSTANCE.Invoke(this.gameObject);
        }
    }
}
