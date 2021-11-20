using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class FollowToTheSide : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
    
        void FixedUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
