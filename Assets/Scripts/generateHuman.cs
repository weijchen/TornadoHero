using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class generateHuman : MonoBehaviour
    {

        private bool isStart;
        public GameObject CreateObject;

        void Start()
        {
            StartCoroutine("WaitAndPrint");
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
}
