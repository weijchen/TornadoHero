using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private GameObject RopePrefab;
        [SerializeField] private GameObject SavedPeople;
        [SerializeField] float callTime = 20.0f;

        private Vector3 currentPosition;
        private Quaternion exitPositionValue;
        private Quaternion exitRotationValue;
        
        private bool leaveHand = false;
        private List<GameObject> ropes = new List<GameObject>();
        private int partCount = 0;
        private int maxPartCount = 150;
        private bool hasCatch = false;
        private bool hasResume = false;
        private Vector3 currPoint = Vector3.zero;
        private float speed = 25.0f;

        private void Update()
        {
            if (leaveHand)
            {
                transform.rotation = exitRotationValue;
                DetectRopeReachMaxLength();
            }
            if (hasCatch)
            {
                ResumePath();
                SavedPeople.SetActive(true);
            }
            if (hasResume)
            {
                DeleteRope();
            }
        }

        private void DetectRopeReachMaxLength()
        {
            if (partCount > maxPartCount)
            {
                DeleteRope();
            }
        }
        
        private void DeleteRope()
        {
            foreach (var rope in ropes)
            {
                Destroy(rope);
            }
            Destroy(gameObject);
        }

        public void StartSpawnRope()
        {
            exitRotationValue = transform.localRotation;
            leaveHand = true;
            StartCoroutine(SpawnRope(0.05f));
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("PlayerB"))
            {
                Destroy(other.gameObject);
                hasCatch = true;
            }
        }

        private void ResumePath()
        {
            if (partCount > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, ropes[partCount-1].transform.position,
                    speed * Time.deltaTime);
                partCount -= 1;
            }
            else
            {
                hasCatch = false;
                hasResume = true;
            }
        }

        IEnumerator SpawnRope(float TimeBetween)
        {
            while (partCount <= maxPartCount && !hasCatch)
            {
                GameObject rope = Instantiate(RopePrefab, transform.position, quaternion.identity);
                ropes.Add(rope);
                partCount += 1;
                yield return new WaitForSeconds(TimeBetween);
            }
        }
    }
}
