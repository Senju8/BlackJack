using Assets.Scripts.System;
using Item;
using NUnit.Framework.Interfaces;
using Player;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private readonly Dictionary<string, ItemImageHolder> itemImageHolders = new();

        private readonly List<ItemData> playerItemData = new();
        
        private float difficulty = 1.0F;
        private bool infiniteMoneyMode = true;

        /// <summary>
        /// ゲームの難易度
        /// </summary>
        public float Difficulty
        {
            get { return this.difficulty; }
            set
            {
                float old = this.Quata;

                this.difficulty = Mathf.Max(0.0F, value);

                // デバッグ
                if (old != this.Quata)
                {
                    UnityEngine.Debug.Log($"難易度が変更されました！（ノルマ額：{old} $ → {this.Quata} $）");
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

        /// <summary>
        /// 無限の所持金モード（デバッグ）
        /// </summary>
        public bool InfiniteMoneyMode
        {
            get { return this.infiniteMoneyMode; }
            set { this.infiniteMoneyMode = value; }
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
            try
            {
                gamePhase.DoInit();
            }
            catch
            {
                UnityEngine.Debug.LogError($"GamePhase（ID: {id}）の初期化に失敗しました…");

                gamePhase.DoDiscard();
            }

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
            if (this.bindingGamePhase.DoStart())
            {
                UnityEngine.Debug.Log($"GamePhase（ID: {id}）が呼び出されました！");
            }
            else
            {
                UnityEngine.Debug.LogError($"GamePhase（ID: {id}）の呼び出しに失敗しました…");
            }

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
                    this.itemImageHolders[ItemImageHolder.GetID(itemImageHolder)] = itemImageHolder;

                    UnityEngine.Debug.Log($"新しいItemImageHolder（Name: {itemImageHolder.Name}, Rarity: {itemImageHolder.Rarity}）が登録されました！");
                }
            }
        }

        /// <summary>
        /// <para>ItemImageHolderを削除する</para>
        /// </summary>
        public bool DeleteItemImageHolder(string name, float rarity)
        {
            if (name == null || !this.itemImageHolders.ContainsKey(ItemImageHolder.GetID(name, rarity)))
                return false;

            // デバッグ
            UnityEngine.Debug.Log($"ItemImageHolder（Name: {name}）が削除されました！");

            return this.itemImageHolders.Remove(ItemImageHolder.GetID(name, rarity));
        }

        /// <summary>
        /// <para>登録されたItemImageHolder</para>
        /// </summary>
        public ItemImageHolder GetItemImageHolder(string name, float rarity)
        {
            if (this.itemImageHolders.ContainsKey(ItemImageHolder.GetID(name, rarity)))
            {
                return this.itemImageHolders[ItemImageHolder.GetID(name, rarity)] ?? ItemImageHolder.EMPTY;
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

        /// <summary>
        /// GamePhaseにイベントを発生させる
        /// </summary>
        public void Invoke(GameObject gameObject, params object[] contexts)
        {
            if (this.bindingGamePhase == null)
                return;

            this.bindingGamePhase.Invoke(gameObject, contexts);
        }

        /// <summary>
        /// <para>プレイヤーのItemDataを増やす</para>
        /// </summary>
        public void AddPlayerItemData(params ItemData[] itemData)
        {
            if (this.playerItemData == null || itemData == null)
                return;

            int totalCount = 0;
            bool hasItemData;

            foreach (ItemData itemDataToAdd in itemData)
            {
                if (itemDataToAdd == null)
                    continue;

                hasItemData = false;

                foreach (ItemData havingItemData in this.playerItemData)
                {
                    if (havingItemData == null)
                        continue;

                    // プレイヤーがItemDataを持っているかどうかを確認する
                    if (itemDataToAdd.Equals(havingItemData))
                    {
                        // ItemData.Countを加算する
                        havingItemData.Count += itemDataToAdd.Count;

                        totalCount = havingItemData.Count;
                        hasItemData = true;

                        break;
                    }
                }

                // ItemDataのリストに追加する
                if (!hasItemData)
                {
                    this.playerItemData.Add(itemDataToAdd);

                    totalCount = itemDataToAdd.Count;
                }

                // デバッグ
                if (itemDataToAdd.Count > 0)
                {
                    UnityEngine.Debug.Log($"プレイヤーにアイテム（Name: {itemDataToAdd.Name}, Rarity: {itemDataToAdd.Rarity:0.00}）を{itemDataToAdd.Count}コ追加しました！（合計：{totalCount}コ）");
                }
            }
        }

        /// <summary>
        /// <para>プレイヤーのItemDataを増やす／減らす</para>
        /// </summary>
        public void IncreasePlayerItemData(ItemData itemData, int count = 1)
        {
            if (this.playerItemData == null || itemData == null || count <= 0)
                return;

            List<ItemData> itemDataToRemove = new(this.playerItemData.Count);

            foreach (ItemData havingItemData in this.playerItemData)
            {
                if (havingItemData != null && havingItemData.Equals(itemData))
                {
                    havingItemData.Count -= count;

                    UnityEngine.Debug.Log($"プレイヤーからアイテム（Name: {havingItemData.Name}, Rarity: {havingItemData.Rarity:0.00}）を{count - havingItemData.Count}コ削除しました！（合計：{havingItemData.Count}コ）");

                    if (havingItemData.Count <= 0)
                    {
                        // 削除するItemDataをマークする
                        itemDataToRemove.Add(havingItemData);
                    }

                    break;
                }
            }

            // ItemDataを削除する
            foreach (ItemData havngItemData in itemDataToRemove)
            {
                this.playerItemData.Remove(havngItemData);
            }
        }

        /// <summary>
        /// <para>プレイヤーからItemDataを取得する</para>
        /// <para>存在しない場合はItemData.EMPTYを返す</para>
        /// </summary>
        public ItemData GetPlayerItemData(string name, float rarity)
        {
            if (this.playerItemData != null && name != null)
            {
                return this.playerItemData.Find(havingItemData => havingItemData != null && havingItemData.Name == name && havingItemData.Rarity == rarity) ?? ItemData.EMPTY;
            }

            return ItemData.EMPTY;
        }

        /// <summary>
        /// <para>プレイヤーからItemDataを取得する</para>
        /// <para>存在しない場合はItemData.EMPTYを返す</para>
        /// </summary>
        public ItemData GetPlayerItemData(ItemData itemData)
        {
            if (this.playerItemData != null && itemData != null)
            {
                return this.playerItemData.Find(havingItemData => havingItemData != null && havingItemData.Equals(itemData)) ?? ItemData.EMPTY;
            }

            return ItemData.EMPTY;
        }

        /// <summary>
        /// <para>プレイヤーからItemDataのリストを取得する</para>
        /// </summary>
        public List<ItemData> GetAllPlayerItemData()
        {
            return new(this.playerItemData);
        }
    }
}
