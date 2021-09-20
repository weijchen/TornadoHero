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
    private Vector3 Foredir;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "PlayerB"))
        {
            StartCoroutine(blowObject(other, true));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "PlayerB_onground") && (other.gameObject.GetComponent<ShouldBlow>().shouldBlow == true))
        {
            StartCoroutine(blowObject(other, true));
        }
        
        if ((other.tag == "car"))
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
            var adjustCenter = new Vector3(0, 3.0f, 0);
            
            if ((other.tag == "PlayerB") || (other.tag == "PlayerB_onground")){
                Foredir = tornadoCenter.position + adjustCenter + other.gameObject.GetComponent<PlayerB>().GetBornVector() - other.transform.position;
            }
            else
            {
                Foredir = tornadoCenter.position + adjustCenter - other.transform.position;
            }

            if (other.tag == "PlayerB")
            {
                other.GetComponent<Rigidbody>().AddForce(Foredir.normalized * pullforce * Time.deltaTime, ForceMode.Acceleration);
            }
            else if (other.tag == "PlayerB_onground")
            {
                other.GetComponent<Rigidbody>().AddForce(Foredir.normalized * pullforce * Time.deltaTime * 0.05f, ForceMode.Acceleration);
            }
            else if (other.tag == "car")
            {
                float pullTimeRandom = other.gameObject.GetComponent<Car>().GetPullForce();
                other.GetComponent<Rigidbody>().AddForce(Foredir.normalized * pullforce * Time.deltaTime * pullTimeRandom, ForceMode.Acceleration);
            }

            if (other.GetComponent<Rigidbody>().velocity.sqrMagnitude > maxV)
            {
                other.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity.normalized * maxV;

            }

            yield return refreshRate;
            StartCoroutine(blowObject(other, shouldPull));
        }
    }
}
