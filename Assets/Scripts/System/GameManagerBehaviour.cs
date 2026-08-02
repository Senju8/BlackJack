using System;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class GameManagerBehaviour : MonoBehaviour
    {
        [Header("スタート画面")]
        [SerializeField] private GameObject startCanvas;

        [Header("難易度セレクト画面")]
        [SerializeField] private GameObject selectCanvas;

        [Header("アイテム購入画面")]
        [SerializeField] private GameObject buyCanvas;

        [Header("アイテム購入画面のアイテムスロット")]
        [SerializeField] private GameObject buyCanvasItemDisplaySlot;

        [Header("ブラックジャック画面")]
        [SerializeField] private GameObject blackjackCanvas;

        [Header("リザルト画面")]
        [SerializeField] private GameObject resultCanvas;

        public GameObject StartCanvas
        {
            get { return this.startCanvas; }
        }

        public GameObject SelectCanvas
        {
            get { return this.selectCanvas; }
        }

        public GameObject BuyCanvas
        {
            get { return this.buyCanvas; }
        }

        public GameObject BuyCanvasItemDisplaySlot
        {
            get { return this.buyCanvasItemDisplaySlot; }
        }

        public GameObject BlackjackCanvas
        {
            get { return this.blackjackCanvas; }
        }

        public GameObject ResultCanvas
        {
            get { return this.resultCanvas; }
        }

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