using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class PeopleAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private float transformDistance = 1.0f;
    
        private Animator animator;
        private bool isGrounded = false;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            SetRigidBodyConstraint();

            if (parent != null)
            {
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
        }

        private void SetRigidBodyConstraint()
        {
            if (isGrounded)
            {
                if (parent != null)
                    parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
