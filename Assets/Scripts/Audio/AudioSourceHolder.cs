using System;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// <para>インスペクターでサウンド（Audio Source）を定義できる</para>
    /// </summary>
    [Serializable]
    public class AudioSourceHolder
    {
        [Header("サウンドの名前")]
        [SerializeField] private string name = "Unknown";

        [Header("サウンドのソース（Audio Source）")]
        [SerializeField] private AudioSource audioSource;

        private GameObject audioObject;

        public string Name
        {
            get { return this.name; }
        }

        public AudioSource AudioSource
        {
            get { return this.audioSource; }
        }

        public AudioSourceHolder(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        /// <summary>
        /// <para>サウンドを再生する</para>
        /// </summary>
        public void Play()
        {
            if (this.Instantiate())
            {
                this.audioSource.Play();

                UnityEngine.Debug.Log($"サウンド（Name: {this.name}）が再生されました！");
            }
        }

        /// <summary>
        /// <para>サウンドを停止する</para>
        /// </summary>
        public void Stop()
        {
            if (this.Instantiate() && this.audioSource.isPlaying)
            {
                this.audioSource.Stop();

                UnityEngine.Debug.Log($"サウンド（Name: {this.name}）が停止されました！");
            }
        }

        /// <summary>
        /// <para>サウンドがアタッチされたGameObjectを生成する</para>
        /// </summary>
        public bool Instantiate()
        {
            if (this.audioSource == null)
                return false;

            if (this.audioObject != null)
                return true;

            // GameObjectを生成する
            this.audioObject = new GameObject($"{this.Name}(Audio)");

            if (this.audioObject != null && audioSource != null)
            {
                AudioSource audioSource = this.audioObject.AddComponent<AudioSource>();

                if (audioSource != null)
                {
                    // サウンドを設定する
                    audioSource.generator = this.audioSource.generator;

                    // インスタンスを入れ替える
                    this.audioSource = audioSource;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <para>サウンドがアタッチされたGameObjectを破棄する</para>
        /// </summary>
        public bool Destroy()
        {
            if (this.audioObject == null)
                return false;

            UnityEngine.Object.Destroy(this.audioObject);

            return true;
        }
    }
}
