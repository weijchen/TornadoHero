using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class BatCapsule : MonoBehaviour
    {

        [SerializeField] private BatFollower _batFollowerPrefab;

        private void Start()
        {
            SpawnBatCapsuleFollower();
        }

        private void SpawnBatCapsuleFollower()
        {
            var follower = Instantiate(_batFollowerPrefab);
            follower.transform.position = transform.position;
            follower.SetFollowTarget(this);
        }
    }
}
