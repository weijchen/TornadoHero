using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class PlayerController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Obstacle")
            {
                PlayerManager.Instance.TurnStunned();            
            }
        }
    }
}
