using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class VelocityDebugger : MonoBehaviour
    {
        [SerializeField] private float maxVelocity = 20.0f;
    
        void Update()
        {
            GetComponent<Renderer>().material.color = ColorForVelocity();
        }

        private Color ColorForVelocity()
        {
            float velocity = GetComponent<Rigidbody>().velocity.magnitude;

            return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
        }
    }
}
