using Assets.Scripts.System;
using UnityEngine;
using Util;

namespace System
{
    /// <summary>
    /// <para>リザルトのフェーズを定義する</para>
    /// </summary>
    public class ResultPhase : GamePhase
    {
        private GameObject canvasObject;

        private GameObject noneDisplayObject;
        private GameObject winDisplayObject;
        private GameObject loseDisplayObject;

        public ResultPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.ResultCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.ResultCanvas);

            if (this.canvasObject != null)
            {
                this.noneDisplayObject = UIUtil.GetChild(this.canvasObject, "None Display");
                this.winDisplayObject = UIUtil.GetChild(this.canvasObject, "Win Display");
                this.loseDisplayObject = UIUtil.GetChild(this.canvasObject, "Lose Display");

                if (this.noneDisplayObject != null)
                    this.noneDisplayObject.SetActive(false);

                if (this.winDisplayObject != null)
                    this.winDisplayObject.SetActive(false);

                if (this.loseDisplayObject != null)
                    this.loseDisplayObject.SetActive(false);
            }

            this.canvasObject.SetActive(false);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;

            switch (this.gameManager.GameResult)
            {
                case ResultPhase.Result.None:
                    if (this.noneDisplayObject != null)
                        this.noneDisplayObject.SetActive(true);

                    break;

                case ResultPhase.Result.Win:
                    if (this.winDisplayObject != null)
                        this.winDisplayObject.SetActive(true);

                    break;

                case ResultPhase.Result.Lose:
                    if (this.loseDisplayObject != null)
                        this.loseDisplayObject.SetActive(true);

                    break;
            }

            this.canvasObject.SetActive(true);
        }

        protected override void Update()
        {
        }

        protected override void Finish()
        {
            if (this.canvasObject == null)
                return;

            if (this.noneDisplayObject != null)
                this.noneDisplayObject.SetActive(false);

            if (this.winDisplayObject != null)
                this.winDisplayObject.SetActive(false);

            if (this.loseDisplayObject != null)
                this.loseDisplayObject.SetActive(false);

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
                    this.gameManager.Call("start");
                    this.gameManager.Play("Select");

                    break;
                case "Next":
                    this.gameManager.Call("shop");
                    this.gameManager.Play("Select");

                    break;
            }
        }

        public enum Result
        {
            None,
            Win,
            Lose
        }
    }
}
