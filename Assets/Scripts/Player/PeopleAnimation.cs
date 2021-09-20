    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimation : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private float transformDistance = 2.0f;
    
     private Animator animator;
     private bool isGrounded = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        SetRigidBodyConstraint();
        
        if (parent.transform.position.y > transformDistance)
        {
            isGrounded = false;
            animator.SetBool("OnGround", false);
        }
        else
        {
            isGrounded = true;
            animator.SetBool("OnGround", true);
        }
    }

    private void SetRigidBodyConstraint()
    {
        if (isGrounded)
        {
            parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
