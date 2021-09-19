using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float stunnedTime = 3.0f;

    private int savedAmount = 0;
    private int deadAmount = 0;
    private bool isStunned = false;
    private float accumTime = 0f;
    public bool canSpawnBat = false;

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

    public void InitiateState()
    {
        savedAmount = 0;
        deadAmount = 0;
    }

    public void TurnStunned()
    {
        isStunned = true;
    }

    public void StunnedRecover()
    {
        accumTime += Time.deltaTime;
        if (accumTime > stunnedTime)
        {
            accumTime = 0f;
            isStunned = false;
        }
    }
}
