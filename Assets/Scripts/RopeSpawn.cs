using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class RopeSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject partPrefab, parentObject;
        [SerializeField] [Range(1, 1000)] private int length = 1;
        [SerializeField] private float partDistance = 0.1f;
        [SerializeField] private bool reset, spawn, snapFirst, snapLast;
    
        void Update()
        {
            if (reset)
            {
                foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Destroy(tmp);
                }
            }
    
            reset = false;
    
            if (spawn)
            {
                Spawn();
    
                spawn = false;
            }
        }
    
        public void Spawn()
        {
            int count = (int) (length / partDistance);
    
            for (int i = 0; i < count; i++)
            {
                GameObject tmp;
    
                tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance * (i + 1), transform.position.z), Quaternion.identity, parentObject.transform);
                tmp.transform.eulerAngles = new Vector3(180, 0, 0);
                tmp.name = parentObject.transform.childCount.ToString();
    
                if (i == 0)
                {
                    Destroy(tmp.GetComponent<CharacterJoint>());
                    if (snapFirst)
                    {
                        tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }
                }
                else
                {
                    tmp.GetComponent<CharacterJoint>().connectedBody =
                        parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
                }
            }
    
            if (snapLast)
            {
                parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>()
                    .constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
