using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class BatFollower : MonoBehaviour
    {
        private BatCapsule _batFollower;
        private Rigidbody _rigidbody;
        private Vector3 _velocity;

        [SerializeField] private float _sensitivity = 100.0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 destination = _batFollower.transform.position;
            _rigidbody.transform.rotation = transform.rotation;

            _velocity = (destination - _rigidbody.transform.position) * _sensitivity;

            _rigidbody.velocity = _velocity;
            //transform.rotation = _batFollower.transform.rotation;
        }

        public void SetFollowTarget(BatCapsule batFollower)
        {
            _batFollower = batFollower;
        }
    }
}
