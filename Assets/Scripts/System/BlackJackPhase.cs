using Assets.Scripts.System;
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

        private PlayerData playerData;
        private DealerData dealerData;

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

            playerData = gameManager.playerData;
            dealerData = gameManager.dealerData;

            playerCards.Setup(playerData, deck);
            dealerCards.Setup(dealerData, deck);
            playerScoreView.Setup(playerData);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Start()
        {
            currentSubPhase = SubPhase.Dealing;
            isInputLocked = true;

            playerData.SetIsPlaying(true);
            dealerData.SetIsPlaying(true);

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
                        currentSubPhase = SubPhase.DealerTurn;
                    }
                    break;

                case SubPhase.DealerTurn:
                    if (dealerData.GetIsPlaying())
                    {
                        if (dealerData.GetScore() < 17)
                        {
                            dealerCards.Hit();
                        }
                        else
                        {
                            dealerData.SetIsPlaying(false);
                        }
                    }
                    else
                    {
                        currentSubPhase = SubPhase.Judge;
                    }
                    break;

                case SubPhase.Judge:
                    JudgeResult();

                    currentSubPhase = SubPhase.Result;
                    break;

                case SubPhase.Result:
                    //結果表示処理

                    break;
            }
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