using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class PlayerB : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private Vector3 BornVector = Vector3.zero;
        private bool isSaved = false;

        [SerializeField] float destroyTime = 20.0f;

        void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            BornVector = new Vector3(0, Random.Range(-8.0f, 8.0f), 0);
            Invoke("DestroyPrefab", destroyTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "Hook")
            {
                isSaved = true;
                _playerManager.AddSavedAmount();
            }
        }

        public Vector3 GetBornVector()
        {
            return BornVector;
        }
    
        private void DestroyPrefab()
        {
            if (!isSaved)
            {
                _playerManager.AddDeadAmount();
            }        
            Destroy(gameObject);
        }
    }
}