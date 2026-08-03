using System;
using UnityEngine;

namespace Audio
{
    public class AudioSourceRegistryBehaviour : MonoBehaviour
    {
        [Header("サウンド")]
        [SerializeField] AudioSourceHolder[] audioSourceHolders;

        void Awake()
        {
            if (this.audioSourceHolders == null)
                return;

            GameManager.INSTANCE.RegisterAudioSourceHolders(this.audioSourceHolders);
        }
    }
}