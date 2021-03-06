using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class PlayerB : MonoBehaviour
    {
        [SerializeField] float destroyTime = 20.0f;
        
        private Vector3 BornVector = Vector3.zero;
        private bool isSaved = false;

        void Start()
        {
            BornVector = new Vector3(0, Random.Range(-8.0f, 8.0f), 0);
            Invoke(nameof(DestroyPrefab), destroyTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Hook"))
            {
                isSaved = true;
                PlayerManager.Instance.AddSavedAmount();
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
                PlayerManager.Instance.AddDeadAmount();
            }        
            Destroy(gameObject);
        }
    }
}
