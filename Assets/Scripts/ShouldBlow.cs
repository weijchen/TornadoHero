using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldBlow : MonoBehaviour
{
    // Start is called before the first frame update
    public bool shouldBlow;
    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        shouldBlow = false;
        startTime = Time.time;
        Debug.Log(shouldBlow);
        Debug.Log(startTime);
    }

    // Update is called once per frame
    void Update()
    {
        float SpeedAdjust = 0.9f;
        if (Time.time - startTime <= 4f)
        {
            transform.Translate(Vector3.back * Time.deltaTime * SpeedAdjust, Space.World);
        }
        // === Adjust here to make the running time longer(now 4s) ===
        if (Time.time - startTime > 4f)
        {
            shouldBlow = true;
        }
    }
}
