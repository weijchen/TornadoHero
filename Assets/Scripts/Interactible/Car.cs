using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float destroyAfterSeconds;

        private float _pullForce;
        private float _timer = 0f;
    
        void Start()
        {
            _pullForce = Random.Range(0.001f, 0.01f);
        }

        private void Update()
        {
            if (_timer < destroyAfterSeconds)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public float GetPullForce()
        {
            return _pullForce;
        }
    }
}
