using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>フェーズを定義する</para>
    /// <para>スタート画面、アイテム購入、ゲーム画面などなど…</para>
    /// </summary>
    public abstract class GamePhase
    {
        private readonly GameManager gameManager;

        private PhaseState phaseState = PhaseState.PRE_INIT;

        public GameManager GameManager
        {
            get { return this.gameManager; }
        }

        public GamePhase(GameManager gameManager)
        {
            this.gameManager = gameManager;
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

        /// <summary>
        /// <para>GamePhaseのイベントを定義する</para>
        /// </summary>
        public abstract void Invoke(GameObject gameObject);

        public void DoInit()
        {
            // 初期化できるかチェック
            if (this.phaseState != PhaseState.PRE_INIT)
                return;

            this.Init();

            // PhaseStateをPRE_STARTに遷移する
            this.phaseState = PhaseState.PRE_START;
        }

        public void DoStart()
        {
            // フェーズを開始できるかチェック
            if (this.phaseState != PhaseState.PRE_START)
                return;

            this.Start();

            // PhaseStateをPOST_STARTに遷移する
            this.phaseState = PhaseState.POST_START;
        }

        public void DoUpdate()
        {
            // フェーズを更新できるかチェック
            if (this.phaseState != PhaseState.POST_START)
                return;

            this.Update();
        }

        public void DoFinish()
        {
            // フェーズを終了できるかチェック
            if (this.phaseState != PhaseState.POST_START)
                return;

            this.Finish();

            // PhaseStateをPRE_STARTに遷移する
            this.phaseState = PhaseState.PRE_START;
        }

        public void DoDestroy()
        {
            // 破棄できるかチェック
            if (this.phaseState == PhaseState.PRE_INIT)
                return;

            this.Finish();
            this.Destroy();

            // PhaseStateをPOST_DESTROYに遷移する
            this.phaseState = PhaseState.POST_DESTROY;
        }

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