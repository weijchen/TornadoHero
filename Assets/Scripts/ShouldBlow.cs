using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class ShouldBlow : MonoBehaviour
    {
        private float startTime;
        private float SpeedAdjust = 0.9f;
        private CapsuleCollider capsuleCollider;

        [SerializeField] private float blowTime = 3.0f;
        public bool shouldBlow;

        void Start()
        {
            shouldBlow = false;
            startTime = Time.time;
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        void Update()
        {
            if (Time.time - startTime <= blowTime)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * SpeedAdjust, Space.World);
            }
            else
            {
                capsuleCollider.radius = 3.0f;
                shouldBlow = true;
            }
        }
    }
}
