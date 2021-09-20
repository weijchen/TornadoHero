using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldBlow : MonoBehaviour
{
    public bool shouldBlow;
    private float startTime;
    float SpeedAdjust = 0.9f;
    [SerializeField] private float blowTime = 3.0f;

    void Start()
    {
        shouldBlow = false;
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime <= blowTime)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * SpeedAdjust, Space.World);
        }
        else
        {
            shouldBlow = true;
        }
    }
}
