using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public enum BGMIndex : int
    {
        Main = 1,
    }
    
    public enum SFXIndex : int
    {
        Hit = 1,
        Die = 2
    }
    
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance = null;

        [SerializeField] private AudioClip[] bgmList;
        [SerializeField] private AudioClip[] sfxList;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayBGM(BGMIndex ind)
        {
            _audioSource.clip = bgmList[(int) ind];
            _audioSource.Play();
        }

        public void PlaySFX(SFXIndex ind, float volume)
        {
            _audioSource.PlayOneShot(sfxList[(int) ind], volume);
        }
    }
}