using Assets.Scripts.System;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>スタートのフェーズを定義する</para>
    /// </summary>
    public class StartPhase : GamePhase
    {
        private GameObject canvasObject;

        public StartPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.StartCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.StartCanvas);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;

            this.canvasObject.SetActive(true);
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
            if (this.canvasObject == null)
                return;

            this.canvasObject.SetActive(false);
        }

        protected override void Destroy()
        {
            if (this.canvasObject == null)
                return;

            UnityEngine.Object.Destroy(this.canvasObject);
        }

        public override void Invoke(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            switch (gameObject.name)
            {
                case "Start":
                    this.gameManager.Call("select");

                    break;
                case "Exit":
                    UnityEngine.Debug.Log("EXIT");

                    break;

            }
        }
    }
}