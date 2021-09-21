using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float stunnedTime = 3.0f;
    [SerializeField] XRDirectInteractor _xrDirectInteractorL;
    [SerializeField] XRDirectInteractor _xrDirectInteractorR;
    [SerializeField] public GameObject stunnedEffect;
    [SerializeField] public GameObject obstacleComingEffect;
    [SerializeField] private bool toSave = false; 

    static private int savedAmount = 0;
    static private int deadAmount = 0;
    static private int hitAmount = 0;
    private static PlayerManager playerManagerInstance;

    private bool isStunned = false;
    private float accumTime = 0f;
    public bool canSpawnBat = false;
    private int finalScore = 0;

    private void Awake()
    {
        if (toSave)
        {
            DontDestroyOnLoad(this);

            if (playerManagerInstance == null)
            {
                playerManagerInstance = this;
            }
            else
            {
                DestroyObject(gameObject);
            }
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
        isStunned = true;
        stunnedEffect.gameObject.SetActive(true);
        obstacleComingEffect.gameObject.SetActive(false);
        _xrDirectInteractorL.enabled = false;
        _xrDirectInteractorR.enabled = false;
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
    }
}
