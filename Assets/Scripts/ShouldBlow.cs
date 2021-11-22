using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class ShouldBlow : MonoBehaviour
    {
        [SerializeField] private float blowTime = 3.0f;
        
        private float SpeedAdjust = 0.9f;
        private float timer = 0f;
        private CapsuleCollider capsuleCollider;

        public bool shouldBlow;

        void Start()
        {
            shouldBlow = false;
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        void Update()
        {
            if (timer <= blowTime)
            {
                timer += Time.deltaTime;
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
