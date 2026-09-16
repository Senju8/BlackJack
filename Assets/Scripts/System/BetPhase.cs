using Assets.Scripts.System;
using Bet;
using Player;
using TMPro;
using UnityEngine;
using Util;

namespace System
{
    /// <summary>
    /// <para>ベットのフェーズを定義する</para>
    /// </summary>
    public class BetPhase : GamePhase
    {
        private GameObject canvasObject;

        private GameObject betObject;
        private GameObject betDisplayObject;
        private GameObject playerDataDisplayObject;

        public BetPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        protected override void Init()
        {
            if (this.gameManagerBehaviour.BetCanvas == null)
                return;

            this.canvasObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.BetCanvas);
            
            this.betObject = UnityEngine.Object.Instantiate(this.gameManagerBehaviour.BetOnlyUIs);
            this.betDisplayObject = UIUtil.GetChild(this.canvasObject, "Left Display/Bet Display");
            this.playerDataDisplayObject = UIUtil.GetChild(this.canvasObject, "Right Display/Game Data Display");

            // ベットUIをセットする
            if (this.betObject != null && this.betDisplayObject != null && this.betObject.transform is RectTransform rectTransform)
            {
                // 位置の調整をする
                rectTransform.SetParent(this.betDisplayObject.transform);
                rectTransform.anchoredPosition = Vector2.zero;

                rectTransform.localScale = Vector3.one;

                UIUtil.InvokeIfPresent<BetButtoms>(this.betObject, betButtoms =>
                {
                    // ベットを確定したときにショップフェーズへ遷移する
                    betButtoms.OnBetConfirmed += betAmount =>
                    {
                        this.gameManager.Call("blackjack");
                    };
                });

                this.betObject.SetActive(true);
            }

            // プレイヤーUIのセットをする
            if (this.canvasObject != null && this.playerDataDisplayObject != null)
            {
                GameObject rightDisplayObject = UIUtil.GetChild(this.canvasObject, "Right Display");

                if (rightDisplayObject != null)
                {
                    this.playerDataDisplayObject.transform.SetParent(this.canvasObject.transform);
                }
            }
            
            this.canvasObject.SetActive(false);
        }

        protected override void Start()
        {
            if (this.canvasObject == null)
                return;

            if (this.playerDataDisplayObject != null)
            {
                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Difficulty/Name Display/Name"), textMeshProUGUI => textMeshProUGUI.text = GameTexts.Get("result.difficulty"));
                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Difficulty/Value Display/Value"), textMeshProUGUI =>
                {
                    float difficulty = this.gameManager.Difficulty;

                    if (difficulty >= 0.5F)
                    {
                        textMeshProUGUI.text = $"{GameTexts.Get("difficulty.easy")}";
                    }
                    else if (difficulty >= 0.1F)
                    {
                        textMeshProUGUI.text = $"{GameTexts.Get("difficulty.normal")}";
                    }
                    else
                    {
                        textMeshProUGUI.text = $"{GameTexts.Get("difficulty.hard")}";
                    }
                });

                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Quota/Name Display/Name"), textMeshProUGUI => textMeshProUGUI.text = GameTexts.Get("result.quota"));
                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Quota/Value Display/Value"), textMeshProUGUI => textMeshProUGUI.text = $"{this.gameManager.Quata} $");

                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Money/Name Display/Name"), textMeshProUGUI => textMeshProUGUI.text = GameTexts.Get("result.money"));
                UIUtil.InvokeIfPresent<TextMeshProUGUI>(UIUtil.GetChild(this.playerDataDisplayObject, "Money/Value Display/Value"), textMeshProUGUI =>
                {
                    textMeshProUGUI.text = $"{this.gameManager.playerData.GetValues()} $";
                });
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
        }
    }
}