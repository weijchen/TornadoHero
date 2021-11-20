using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class Bat : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "Obstacle")
            {
                _audioSource.Play();
            }
        }
    }
}
