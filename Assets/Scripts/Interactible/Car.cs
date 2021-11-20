using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float destroyAfterSeconds;
        [SerializeField] private float pullForce;
    
        void Start()
        {
            pullForce = Random.Range(0.001f, 0.01f);
            Destroy(gameObject, destroyAfterSeconds);
        }

        public float GetPullForce()
        {
            return pullForce;
        }
    }
}
