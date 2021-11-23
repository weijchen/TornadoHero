using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Team13.Round1.TornadoHero
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance = null;

        [SerializeField] private float stunnedTime = 3.0f;
        [SerializeField] XRDirectInteractor _xrDirectInteractorL;
        [SerializeField] XRDirectInteractor _xrDirectInteractorR;
        [SerializeField] public GameObject stunnedEffect;
        [SerializeField] public GameObject obstacleComingEffect;
        [SerializeField] private bool toSave = false; 

        private static int savedAmount = 0;
        private static int deadAmount = 0;
        private static int hitAmount = 0;
        private static int currCombo = 0;
        private static int totalCombo = 0;

        private bool isStunned = false;
        private float accumTime = 0f;
        private int finalScore = 0;
        public bool canSpawnBat = false;

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

        void Update()
        {
            if (isStunned)
            {
                StunnedRecover();
            }
        }

        public void AddSavedAmount()
        {
            savedAmount += 1;
        }

        public void AddDeadAmount()
        {
            deadAmount += 1;
        }

        public void MinusDeadAmount()
        {
            deadAmount -= 1;
        }

        public int GetSavedAmount()
        {
            return savedAmount;
        }

        public int GetDeadAmount()
        {
            return deadAmount;
        }

        public int GetHitAmount()
        {
            return hitAmount;
        }

        public void InitiateState()
        {
            savedAmount = 0;
            deadAmount = 0;
        }

        public void TurnStunned()
        {
            totalCombo += currCombo;
            Debug.Log(totalCombo);
            currCombo = 0;
            isStunned = true;
            stunnedEffect.gameObject.SetActive(true);
            obstacleComingEffect.gameObject.SetActive(false);
            _xrDirectInteractorL.enabled = false;
            _xrDirectInteractorR.enabled = false;
        }

        public int GetComboAmount()
        {
            return totalCombo + currCombo;
        }

        public void StunnedRecover()
        {
            accumTime += Time.deltaTime;
            if (accumTime > stunnedTime)
            {
                accumTime = 0f;
                isStunned = false;
                stunnedEffect.gameObject.SetActive(false);
                _xrDirectInteractorL.enabled = true;
                _xrDirectInteractorR.enabled = true;
            }
        }

        public void AddHitAmount()
        {
            hitAmount += 1;
            currCombo += 1;
        }
    }
}
