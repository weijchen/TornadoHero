using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public enum BGMIndex : int
    {
        Main = 0,
        End = 1
    }
    
    public enum SFXIndex : int
    {
        Tornado = 0,
        Hit = 1,
        HookSpawn = 2
    }
    
    public enum VOIndex : int
    {
        Intro = 0,
        News = 1,
        Ding = 2,
        OneStep = 3,
        ScreamOne = 4,
        ScreamTwo = 5,
        EquipmentOne = 6,
        EquipmentTwo = 7,
        ManySteps = 8
    }
    
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance = null;

        [SerializeField] private AudioClip[] bgmList;
        [SerializeField] private AudioClip[] sfxList;
        [SerializeField] private AudioClip[] voList;

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

        public void PlaySFX(SFXIndex ind, float volume = 1f)
        {
            _audioSource.PlayOneShot(sfxList[(int) ind], volume);
        }
        
        public void PlayVO(VOIndex ind, float volume = 1f)
        {
            _audioSource.PlayOneShot(voList[(int) ind], volume);
        }
    }
}