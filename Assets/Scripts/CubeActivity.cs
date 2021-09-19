using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeActivity : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Vector3 _bornVector = Vector3.zero;
    

    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _bornVector = new Vector3(0, Random.Range(-8.0f, 8.0f), 0);
    }

    public Vector3 GetBornVector()
    {
        return _bornVector;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Hook")
        {
            _playerManager.AddSavedAmount();
        }
    }
}
