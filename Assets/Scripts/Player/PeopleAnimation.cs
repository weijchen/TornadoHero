    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("Running");
    }

    void Update()
    {
        if (gameObject.transform.position.y > 2)
        {
            animator.SetBool("OnGround", false);
        }
        
        if (gameObject.transform.position.y <= 2)
        {
            animator.SetBool("OnGround", true);
        }
    }
}
