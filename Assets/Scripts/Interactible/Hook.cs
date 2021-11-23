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
        [Header("Prefabs")]
        [SerializeField] private GameObject RopePrefab;
        [SerializeField] private GameObject SavedPeople;
        [SerializeField] private int maxPartCount = 150;
        
        [Header("Movement")]
        [SerializeField] private float resumeDuration = 5.0f;

        private Vector3 startPosition;
        private Vector3 catchPosition;
        private Quaternion exitPositionValue;
        private Quaternion exitRotationValue;
        
        private List<GameObject> ropes = new List<GameObject>();
        private int partCount = 0;
        private bool leaveHand = false;
        private bool hasCatch = false;
        private bool hasResume = false;

        private void Start()
        {
            startPosition = PlayerManager.Instance.transform.position;
        }

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
                catchPosition = other.transform.position;
                SoundManager.Instance.PlaySFX(SFXIndex.HookSuccess);
                Destroy(other.gameObject);
                hasCatch = true;
            }
        }

        private void ResumePath()
        {
            StartCoroutine(StartResumePath());
        }

        IEnumerator StartResumePath()
        {
            float timeElapsed = 0f;

            while (timeElapsed < resumeDuration)
            {
                transform.position = Vector3.Lerp(catchPosition, startPosition, timeElapsed / resumeDuration);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            hasCatch = false;
            hasResume = true;
            transform.position = startPosition;
        }

        IEnumerator SpawnRope(float timeBetween)
        {
            while (partCount <= maxPartCount && !hasCatch)
            {
                GameObject rope = Instantiate(RopePrefab, transform.position, quaternion.identity);
                ropes.Add(rope);
                partCount += 1;
                yield return new WaitForSeconds(timeBetween);
            }
        }
    }
}
