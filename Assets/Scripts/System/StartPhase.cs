using Assets.Scripts.System;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>スタートのフェーズを定義する</para>
    /// </summary>
    public class StartPhase : GamePhase
    {
        public StartPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
        }

        protected override void Destroy()
        {
        }

        public override void Invoke(GameObject gameObject)
        {
        }
    }
}