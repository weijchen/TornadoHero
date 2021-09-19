using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateHuman : MonoBehaviour
{

    //private IEnumerator coroutine;
    private bool isStart;
    public GameObject CreateObject;

    void Start()
    {
        StartCoroutine("WaitAndPrint");
    }

    void Update()
    {
        
    }

    private IEnumerator WaitAndPrint()
    {
        for (; ; )
        {
            GameObject.Instantiate(CreateObject, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(Random.Range(5f,12f));
        }    
    }
}
