using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager _playerManager;
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            _playerManager.TurnStunned();            
        }
    }
}
