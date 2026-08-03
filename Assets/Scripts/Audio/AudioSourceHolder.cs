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
        /// <para>サウンドがアタッチされたGameObjectを生成する</para>
        /// </summary>
        private bool Instantiate()
        {
            if (this.audioSource == null)
                return false;

            if (this.audioObject != null)
                return true;

            // GameObjectを生成する
            this.audioObject = new GameObject($"Audio:{this.Name}");

            if (this.audioObject != null && audioSource != null)
            {
                AudioSource audioSource = this.audioObject.AddComponent<AudioSource>();
                
                // サウンドを設定
                audioSource.generator = this.audioSource.generator;

                this.audioSource = audioSource;

                return true;
            }

            return false;
        }
    }
}
