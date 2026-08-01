using System.Collections.Generic;
using System.Diagnostics;
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

        private string bindingGamePhaseId;
        private GamePhase bindingGamePhase;

        private GameManager() { }

        /// <summary>
        /// <para>GameManagerの初期化</para>
        /// </summary>
        public void Init()
        {
            // フェーズの登録
            this.Register("select", new SelectPhase(this));
            this.Register("buy", new BuyPhase(this));
            this.Register("blackjack", new BlackjackPhase(this));
            this.Register("result", new ResultPhase(this));

            this.Call("select");
        }

        /// <summary>
        /// <para>GameManagerの更新</para>
        /// </summary>
        public void Update()
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
