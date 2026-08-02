using Assets.Scripts.System;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>フェーズを定義する</para>
    /// <para>スタート画面、アイテム購入、ゲーム画面などなど…</para>
    /// </summary>
    public abstract class GamePhase
    {
        public readonly GameManager gameManager;
        public readonly GameManagerBehaviour gameManagerBehaviour;

        private PhaseState phaseState = PhaseState.PRE_INIT;

        public GamePhase(GameManager gameManager, GameManagerBehaviour gameManagerBehaviour)
        {
            this.gameManager = gameManager;
            this.gameManagerBehaviour = gameManagerBehaviour;
        }

        /// <summary>
        /// <para>GamePhaseの初期化を定義する</para>
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// <para>フェーズの開始を定義する</para>
        /// </summary>
        protected abstract void Start();

        /// <summary>
        /// <para>フェーズの更新を定義する</para>
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// <para>フェーズの終了を定義する</para>
        /// </summary>
        protected abstract void Finish();

        /// <summary>
        /// <para>GamePhaseの破棄を定義する</para>
        /// </summary>
        protected abstract void Destroy();

        public bool DoInit()
        {
            // 初期化できるかチェック
            if (this.phaseState != PhaseState.PRE_INIT)
                return false;

            this.Init();

            // PhaseStateをPRE_STARTに遷移する
            this.phaseState = PhaseState.PRE_START;

            return true;
        }

        public bool DoStart()
        {
            // フェーズを開始できるかチェック
            if (this.phaseState != PhaseState.PRE_START)
                return false;

            this.Start();

            // PhaseStateをPOST_STARTに遷移する
            this.phaseState = PhaseState.POST_START;

            return true;
        }

        public bool DoUpdate()
        {
            // フェーズを更新できるかチェック
            if (this.phaseState != PhaseState.POST_START)
                return false;

            this.Update();

            return true;
        }

        public bool DoFinish()
        {
            // フェーズを終了できるかチェック
            if (this.phaseState != PhaseState.POST_START)
                return false;

            this.Finish();

            // PhaseStateをPRE_STARTに遷移する
            this.phaseState = PhaseState.PRE_START;

            return true;
        }

        public bool DoDestroy()
        {
            // 破棄できるかチェック
            if (this.phaseState == PhaseState.PRE_INIT)
                return false;

            this.Finish();
            this.Destroy();

            // PhaseStateをPOST_DESTROYに遷移する
            this.phaseState = PhaseState.POST_DESTROY;

            return true;
        }

        /// <summary>
        /// GamePhaseを強制的に破棄する
        /// </summary>
        public void DoDiscard()
        {
            this.phaseState = PhaseState.POST_DESTROY;
        }

        /// <summary>
        /// <para>GamePhaseのイベントを定義する</para>
        /// </summary>
        public virtual void Invoke(GameObject gameObject) { }

        /// <summary>
        /// GamePhaseの状態を定義する
        /// </summary>
        private enum PhaseState
        {
            NONE,
            PRE_INIT,
            PRE_START,
            POST_START,
            POST_DESTROY
        }
    }

}