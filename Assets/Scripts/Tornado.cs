using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform tornadoCenter;
    public float pullforce;
    public float refreshRate;
    public float maxV;

    private float fixedDeltaTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerB")
        {
            StartCoroutine(blowObject(other, true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerB")
        {
            StartCoroutine(blowObject(other, false));
        }
    }

    IEnumerator blowObject(Collider other, bool shouldPull)
    {
        if (shouldPull)
        {
            // -- TODO: wanna implement a random center --
            
            var adjustCenter = new Vector3(0, 3.0f, 0);
            Vector3 Foredir = tornadoCenter.position + adjustCenter + other.gameObject.GetComponent<CubeActivity>().GetBornVector() - other.transform.position;
            other.GetComponent<Rigidbody>().AddForce(Foredir.normalized * pullforce * Time.deltaTime, ForceMode.Acceleration);

            if (other.GetComponent<Rigidbody>().velocity.sqrMagnitude > maxV)
            {
                other.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity.normalized * maxV;

            }

            yield return refreshRate;
            StartCoroutine(blowObject(other, shouldPull));
        }
    }
}
