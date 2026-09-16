using UnityEngine;

namespace System
{
    /// <summary>
    /// GameObjectを保持するホルダー
    /// </summary>
    [Serializable]
    public class GameObjectHolder
    {
        /// <summary>
        /// 空のGameObjectHolder
        /// </summary>
        public static readonly GameObjectHolder EMPTY = new EmptyObjectHolder();

        [Header("GameObjectのID")]
        [SerializeField] private string _id;

        [Header("GameObject")]
        [SerializeField] private GameObject _gameObject;

        public string Id
        {
            get { return this._id; }
        }

        public GameObjectHolder(string id, GameObject gameObject)
        {
            this._id = gameObject ? id : "";
            this._gameObject = gameObject; 
        }

        public GameObject Instantiate()
        {
            return this.Instantiate(Vector3.zero);
        }

        public GameObject Instantiate(Vector3 position)
        {
            return this.Instantiate(position, Quaternion.identity);
        }

        /// <summary>
        /// このホルダーからGameObjectを生成する
        /// </summary>
        public GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
            if (this._gameObject)
            {
                return UnityEngine.Object.Instantiate(this._gameObject, position, rotation);
            }

            return null;
        }

        /// <summary>
        /// このホルダーが空でないかチェックする
        /// </summary>
        public static bool Validate(GameObjectHolder holder)
        {
            return !string.IsNullOrEmpty(holder.Id) && holder._gameObject;
        }

        private class EmptyObjectHolder : GameObjectHolder
        {
            public new string Id
            {
                get { return ""; }
            }

            public EmptyObjectHolder() : base("", null) {}
        }
    }
}
