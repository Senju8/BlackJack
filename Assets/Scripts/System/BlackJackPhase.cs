using Assets.Scripts.System;
using Bet;
using Cards;
using Player;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>ブラックジャックのフェーズを定義する</para>
    /// </summary>
    public class BlackjackPhase : GamePhase
    {
        /// <summary>
        /// ブラックジャック内での細かいフェーズ分け
        /// 
        /// プレイヤの入力無視などのために用いる
        /// </summary>
        private enum SubPhase
        {
            Bet,        // ベット処理
            Dealing,    // 最初の2枚配り
            PlyaerTurn, // プレイヤターン
            DealerTurn, // ディーラーターン
            Judge,      // バーストしていないか、互いにスライドしたかなどの判定
            Result,     // 結果の話
        }

        private SubPhase currentSubPhase;

        /// <summary>
        /// アニメーション中などに入力を一時的に停止させるフラグ
        /// </summary>
        private bool isInputLocked = true;

        private Deck deck;
        private PlayerCards playerCards;
        private DealerCards dealerCards;
        private PlayerScoreView playerScoreView;
        private DealerScoreView dealerScoreView;

        private PlayerData playerData;
        private DealerData dealerData;

        private GameObject blackJackOnlyUIs;

        private GameObject betOnlyUIs;
        private BetButtoms betButtoms;

        /// <summary>
        /// ディーラーのカードめくり処理が進行中か
        /// 
        /// Update内でコルーチンを重複しての呼び出しを防ぐために用いる
        /// </summary>
        private bool isDealerCardsOpening = false;


        public BlackjackPhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour) : base(gameManager, gameManagerBehaviour) { }

        /// <summary>
        /// 参照の取得/初期セットアップ
        /// </summary>
        protected override void Init()
        {
            deck = gameManagerBehaviour.Deck;
            playerCards = gameManagerBehaviour.PlayerCards;
            dealerCards = gameManagerBehaviour.DealerCards;
            playerScoreView = gameManagerBehaviour.PlayerScoreView;
            dealerScoreView = gameManagerBehaviour.DealerScoreView;
            blackJackOnlyUIs = gameManagerBehaviour.BlackJackOnlyUIs;

            betOnlyUIs = gameManagerBehaviour.BetOnlyUIs;
            betButtoms = gameManagerBehaviour.BetButtoms;

            playerData = gameManager.playerData;
            dealerData = gameManager.dealerData;

            playerCards.Setup(playerData, deck);
            dealerCards.Setup(dealerData, deck);
            playerScoreView.Setup(playerData);
            dealerScoreView.Setup(dealerData);
            dealerScoreView.SetActiveText(false);
            blackJackOnlyUIs.SetActive(false);
            betOnlyUIs.SetActive(false);

            betButtoms.OnBetConfirmed += OnBetConfirmed;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Start()
        {
            blackJackOnlyUIs.SetActive(false);
            betOnlyUIs.SetActive(true);
            betButtoms.ResetInput();

            currentSubPhase = SubPhase.Bet;
            isInputLocked = true;

            playerData.SetIsPlaying(true);
            dealerData.SetIsPlaying(true);

            // デモ:プレイヤの金額をセット
            playerData.SetValues(10000);
        }

        private void OnBetConfirmed(int betAmount)
        {
            if(currentSubPhase != SubPhase.Bet)
            {
                return;
            }

            betOnlyUIs.SetActive(false);
            blackJackOnlyUIs.SetActive(true);

            currentSubPhase = SubPhase.Dealing;

            deck.InitializeDeck();
            deck.Shuffle();

            playerData.SetCard(new System.Collections.Generic.List<CardsManager.Card>());
            dealerData.SetCard(new System.Collections.Generic.List<CardsManager.Card>());

            playerCards.DrawCard(2);
            dealerCards.DrawInitialCards();

            currentSubPhase = SubPhase.PlyaerTurn;
            isInputLocked = false;
        }

        protected override void Update()
        {
            switch(currentSubPhase)
            {
                case SubPhase.PlyaerTurn:
                    if(!playerData.GetIsPlaying())
                    {
                        isInputLocked = true;
                        if(playerData.GetScore() > 21)
                        {
                            currentSubPhase = SubPhase.Judge;
                            break;
                        }
                        currentSubPhase = SubPhase.DealerTurn;
                    }

                    break;

                case SubPhase.DealerTurn:
                    //ディーラーの処理が未開始ならコルーチンをスタートさせる
                    if(!isDealerCardsOpening)
                    {
                        isDealerCardsOpening = true;
                        gameManagerBehaviour.StartCoroutine(DealerTurnRoutine());
                    }
                     break;

                case SubPhase.Judge:
                    JudgeResult();

                    currentSubPhase = SubPhase.Result;
                    break;

                case SubPhase.Result:
                    //結果表示処理

                    blackJackOnlyUIs.SetActive(false);
                    playerCards.ClearCards();

                    dealerCards.ClearCards();

                    GameManager.INSTANCE.Call("result");
                    break;
            }
        }

        /// <summary>
        /// ディーラーの思考/カードめくりを行うコルーチン
        /// </summary>
        /// <returns></returns>
        private System.Collections.IEnumerator DealerTurnRoutine()
        {
            while(dealerData.GetScore() < 17)
            {
                dealerCards.Hit();
            }
            
            dealerData.SetIsPlaying(false);

            // ディーラーのカードを一定時間ごとにめくる
            yield return gameManagerBehaviour.StartCoroutine(dealerCards.CardsOpen(0.7f));

            yield return new WaitForSeconds(0.5f);

            dealerScoreView.SetActiveText(true);

            yield return new WaitForSeconds(2f);

            currentSubPhase = SubPhase.Judge;
            isDealerCardsOpening = false;
        }


        public void TryHit()
        {
            if (!CanPlayerAct())
            {
                return;
            }

            isInputLocked = true;
            playerCards.Hit();
            isInputLocked = false;
            Debug.Log("ヒット終了");
        }

        public void TryStand()
        {
            if (!CanPlayerAct())
            {
                return;
            }

            isInputLocked = true;
            playerCards.Stand();
            isInputLocked = false;
        }

        /// <summary>
        /// プレイヤの操作の判定
        /// </summary>
        /// <returns></returns>
        private bool CanPlayerAct()
        {
            return !isInputLocked
                && currentSubPhase == SubPhase.PlyaerTurn
                && playerData.GetIsPlaying();
        }

        private void JudgeResult()
        {
            bool playerBurst = ScoreCalclator.IsBurst(playerData.GetCard());
            bool dealerBurst = ScoreCalclator.IsBurst(dealerData.GetCard());
            int playerScore = playerData.GetScore();
            int dealerScore = dealerData.GetScore();

            if(playerBurst)
            {
                // プレイヤ負け処理
                Debug.Log("プレイヤの負け");
            }
            else if(dealerBurst || playerScore > dealerScore)
            {
                // プレイヤ勝ち
                Debug.Log("プレイヤの勝ち");
            }
            else if(playerScore < dealerScore)
            {
                // プレイヤ負け
                Debug.Log("プレイヤの負け");
            }
            else
            {
                // 引き分け
                Debug.Log("ひきわけ");
            }
        }   

        protected override void Finish()
        {
            Debug.Log("リザルトフェーズへ移行");
        }

        protected override void Destroy()
        {
            deck = null;
            playerCards = null;
            dealerCards = null;
            playerScoreView = null;
            playerData = null;
            dealerData = null;
        }

        /// <summary>
        /// UIの表示/非表示を切り替える
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Invoke(GameObject gameObject)
        {
            
        }
    }
}