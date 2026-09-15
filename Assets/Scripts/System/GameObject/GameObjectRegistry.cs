using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class GameObjectRegistry : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private GameObjectHolder[] holders;

        public void Awake()
        {
            GameManager.INSTANCE.RegisterGameObjectHolders(holders);
        }
    }
}