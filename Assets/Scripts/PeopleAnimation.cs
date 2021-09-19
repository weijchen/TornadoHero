    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator peopleAni;

    void Start()
    {
        peopleAni = this.gameObject.GetComponent<Animator>();
        //peopleAni.applyRootMotion = false;
        peopleAni.Play("Running");
        //peopleAni.SetBool("OnGround", true);

        //Debug.Log(peopleAni);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject.transform.position.y);
        if (this.gameObject.transform.position.y > 2  )
        {
            peopleAni.SetBool("OnGround", false);
            
            //Debug.Log("false");
        }
        if (this.gameObject.transform.position.y <= 2)
        {
            peopleAni.SetBool("OnGround", true);
            //Debug.Log("true");
        }
    }
}
