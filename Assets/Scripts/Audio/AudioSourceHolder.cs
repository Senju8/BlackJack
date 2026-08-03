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
            if (this.audioSource != null)
            {
                this.audioSource.Play();
            }
        }
    }
}
