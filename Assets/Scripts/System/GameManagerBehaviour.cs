using System;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class GameManagerBehaviour : MonoBehaviour
    {
        void Awake()
        {
            GameManager.INSTANCE.Init(this);
        }

        void Update()
        {
            GameManager.INSTANCE.Update(this);
        }
    }
}