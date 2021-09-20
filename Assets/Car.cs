using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds;
    [SerializeField] private float pullForce;
    
    void Start()
    {
        pullForce = Random.Range(0.001f, 0.01f);
        Destroy(this.gameObject, destroyAfterSeconds);
    }

    public float GetPullForce()
    {
        return pullForce;
    }
}
