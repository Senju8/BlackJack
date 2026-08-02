using Assets.Scripts.System;
using Item;
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

        public readonly PlayerData playerData;
        public readonly DealerData dealerData;

        private string bindingGamePhaseId;
        private GamePhase bindingGamePhase;

        public float difficulty = 1.0F;

        private readonly Dictionary<string, ItemImageHolder> itemImageHolders = new();

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

        /// <summary>
        /// ゲームのノルマ（難易度依存）
        /// </summary>
        public int Quata
        {
            get { return (int) (600000.0D * this.difficulty); }
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
            this.RegisterGamePhase("start", new StartPhase(this, gameManagerBehaviour));
            this.RegisterGamePhase("select", new SelectPhase(this, gameManagerBehaviour));
            this.RegisterGamePhase("shop", new ShopPhase(this, gameManagerBehaviour));
            this.RegisterGamePhase("blackjack", new BlackjackPhase(this, gameManagerBehaviour));
            this.RegisterGamePhase("result", new ResultPhase(this, gameManagerBehaviour));

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
        public bool RegisterGamePhase(string id, GamePhase gamePhase)
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
            UnityEngine.Debug.Log($"GamePhase（ID: {id}）が登録されました！");

            return true;
        }

        /// <summary>
        /// <para>GamePhaseを削除する</para>
        /// </summary>
        public bool DeleteGamePhase(string id)
        {
            if (id == null || !this.gamePhases.ContainsKey(id))
                return false;

            // GamePhaseの破棄
            this.gamePhases[id]?.DoDestroy();

            // デバッグ
            UnityEngine.Debug.Log($"GamePhase（ID: {id}）が削除されました！");

            return this.gamePhases.Remove(id);
        }

        /// <summary>
        /// <para>登録されたGamePhaseを返す</para>
        /// </summary>
        public T GetPhase<T>(string id) where T : GamePhase
        {
            if (id == null || !this.gamePhases.ContainsKey(id))
                return null;

            return this.gamePhases[id] as T;
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
            UnityEngine.Debug.Log($"GamePhase（ID: {id}）が呼び出されました！");

            return true;
        }

        /// <summary>
        /// <para>ItemImageHolderを登録する</para>
        /// </summary>
        public void RegisterItemImageHolders(ItemImageHolder[] itemImageHolders)
        {
            foreach (ItemImageHolder itemImageHolder in itemImageHolders)
            {
                if (itemImageHolder != null && itemImageHolder.Name != null)
                {
                    this.itemImageHolders[itemImageHolder.Name] = itemImageHolder;

                    UnityEngine.Debug.Log($"新しいItemImageHolder（Name: {itemImageHolder.Name}）が登録されました！");
                }
            }
        }

        /// <summary>
        /// <para>ItemImageHolderを削除する</para>
        /// </summary>
        public bool DeleteItemImageHolder(string name)
        {
            if (name == null || !this.itemImageHolders.ContainsKey(name))
                return false;

            // デバッグ
            UnityEngine.Debug.Log($"ItemImageHolder（Name: {name}）が削除されました！");

            return this.itemImageHolders.Remove(name);
        }

        /// <summary>
        /// <para>登録されたItemImageHolder</para>
        /// </summary>
        public ItemImageHolder GetItemImageHolder(string name)
        {
            if (this.itemImageHolders.ContainsKey(name))
            {
                return this.itemImageHolders[name] ?? ItemImageHolder.EMPTY;
            }

            return ItemImageHolder.EMPTY;
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
