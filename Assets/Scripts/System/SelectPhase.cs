using Assets.Scripts.System;
using UnityEngine;
using UnityEngine.UI;

namespace System
{
    /// <summary>
    /// <para>難易度の選択のフェーズを定義する</para>
    /// </summary>
    public class SelectPhase : GamePhase
    {
        private GameObject canvasObject;

        public SelectPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.SelectCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.SelectCanvas);
            this.canvasObject.SetActive(false);
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
                case "Easy":
                    this.gameManager.Difficulty = 1.0F;

                    break;
                case "Normal":
                    this.gameManager.Difficulty = 2.0F;

                    break;
                case "Hard":
                    this.gameManager.Difficulty = 3.0F;

                    break;
            }
        }
    }
}
