using System;
using Unity.VisualScripting;
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
        [SerializeField] private GameObject shopCanvas;

        [Header("アイテム購入画面のアイテムスロット")]
        [SerializeField] private GameObject buyCanvasItemDisplaySlot;

        [Header("ブラックジャック画面")]
        [SerializeField] private GameObject blackjackCanvas;

        [Header("リザルト画面")]
        [SerializeField] private GameObject resultCanvas;

        [Header("ブラックジャック関連")]
        [SerializeField] private Cards.Deck deck;
        [SerializeField] private Cards.PlayerCards playerCards;
        [SerializeField] private Cards.DealerCards dealerCards;
        [SerializeField] private Player.PlayerScoreView playerScoreView;
        [SerializeField] private Player.DealerScoreView dealerScoreView;
        [SerializeField] private GameObject blackJackOnlyUIs;

        [Header("ベット関連")]
        [SerializeField] private GameObject betOnlyUIs;
        [SerializeField] private Bet.BetButtoms betButtoms;

        public GameObject StartCanvas
        {
            get { return this.startCanvas; }
        }

        public GameObject SelectCanvas
        {
            get { return this.selectCanvas; }
        }

        public GameObject ShopCanvas
        {
            get { return this.shopCanvas; }
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


        public Cards.Deck Deck => deck;
        public Cards.PlayerCards PlayerCards => playerCards;
        public Cards.DealerCards DealerCards => dealerCards;
        public Player.PlayerScoreView PlayerScoreView => playerScoreView;
        public Player.DealerScoreView DealerScoreView => dealerScoreView;
        public GameObject BlackJackOnlyUIs => blackJackOnlyUIs;
        

        public GameObject BetOnlyUIs => betOnlyUIs;
        public Bet.BetButtoms BetButtoms => betButtoms;

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