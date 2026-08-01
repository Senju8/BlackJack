using Assets.Scripts.System;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>ゲームの進行を管理する</para>
    /// </summary>
    public class GameManager
    {
        public static readonly GameManager INSTANCE = new();

        private readonly Dictionary<string, GamePhase> gamePhases = new();

        public PlayerData playerData;
        public DealerData dealerData;

        private string bindingGamePhaseId;
        private GamePhase bindingGamePhase;

        private float difficulty = 1.0F;

        /// <summary>
        /// ゲームの難易度
        /// </summary>
        public float Difficulty
        {
            get { return this.difficulty; }
            set
            {
                float old = this.difficulty;

                this.difficulty = Mathf.Max(0.0F, value);

                // デバッグ
                if (old != this.difficulty)
                {
                    UnityEngine.Debug.Log($"難易度が変更されました！{old:0.00} → {this.difficulty:0.00}");
                }
            }
        }

        private GameManager()
        {
            // プレイヤー、ディーラーの初期化
            this.playerData = new(this);
            this.dealerData = new(this);
        }

        /// <summary>
        /// <para>GameManagerの初期化</para>
        /// </summary>
        public void Init(GameManagerBehaviour gameManagerBehaviour)
        {
            // フェーズの登録
            this.Register("start", new StartPhase(this, gameManagerBehaviour));
            this.Register("select", new SelectPhase(this, gameManagerBehaviour));
            this.Register("buy", new BuyPhase(this, gameManagerBehaviour));
            this.Register("blackjack", new BlackjackPhase(this, gameManagerBehaviour));
            this.Register("result", new ResultPhase(this, gameManagerBehaviour));

            this.Call("start");
        }

        /// <summary>
        /// <para>GameManagerの更新</para>
        /// </summary>
        public void Update(GameManagerBehaviour gameManagerBehaviour)
        {

            if (this.bindingGamePhase != null)
            {
                this.bindingGamePhase.DoUpdate();
            }
        }

        /// <summary>
        /// <para>GamePhaseを登録する</para>
        /// </summary>
        public bool Register(string id, GamePhase gamePhase)
        {
            if (id == null || gamePhase == null)
                return false;

            // IDが存在する場合はそのGamePhaseは破棄する
            if (this.gamePhases.ContainsKey(id))
            {
                this.gamePhases[id]?.DoDestroy();
            }

            // GamePhaseの登録
            this.gamePhases[id] = gamePhase;

            // GamePhaseの初期化
            gamePhase.DoInit();

            // デバッグ
            UnityEngine.Debug.Log($"GamePhase（id: {id}）が登録されました！");

            return true;
        }

        /// <summary>
        /// <para>GamePhaseを削除する</para>
        /// </summary>
        public bool Delete(string id)
        {
            if (id == null || !this.gamePhases.ContainsKey(id))
                return false;

            // GamePhaseの破棄
            this.gamePhases[id]?.DoDestroy();

            // デバッグ
            UnityEngine.Debug.Log($"GamePhase（id: {id}）が削除されました！");

            return this.gamePhases.Remove(id);
        }

        /// <summary>
        /// <para>登録されたGamePhaseを呼び出す</para>
        /// </summary>
        public bool Call(string id)
        {
            if (id == null || !this.gamePhases.ContainsKey(id) || this.gamePhases[id] == null)
                return false;

            // フェーズを終了する
            if (this.bindingGamePhase != null)
            {
                this.bindingGamePhase.DoFinish();
            }

            this.bindingGamePhaseId = id;
            this.bindingGamePhase = this.gamePhases[id];
            
            // フェーズを開始する
            this.bindingGamePhase.DoStart();

            // デバッグ
            UnityEngine.Debug.Log($"GamePhase（id: {id}）が呼び出されました！");

            return true;
        }

        /// <summary>
        /// GamePhaseにイベントを発生させる
        /// </summary>
        public void Invoke(GameObject gameObject)
        {
            if (this.bindingGamePhase == null)
                return;

            this.bindingGamePhase.Invoke(gameObject);
        }
    }
}
