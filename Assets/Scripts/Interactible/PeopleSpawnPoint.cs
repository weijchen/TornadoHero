using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class PeopleSpawnPoint : MonoBehaviour
    {
        [SerializeField] GameObject peoplePrefab;
        [SerializeField] float maxXOffset = 10.0f;
        [SerializeField] float spawnTime = 2.0f;
    
        private PlayerManager _playerManager;
        public bool coroutineIsRunning = true;
    
        void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }
    
        IEnumerator SpawnPeopleCo(float timeBetween)
        {
            if (coroutineIsRunning)
            {
                SpawnPeople();
                yield return new WaitForSeconds(timeBetween);
            }
        }
    
        public void SpawnPeople()
        {
            Vector3 offset = Vector3.zero;
            float randomXOffset = Random.Range(-maxXOffset, maxXOffset);
            offset += new Vector3(randomXOffset, 0, 0);
            
            Vector3 spawnPosition = transform.position;
            spawnPosition.x = (transform.position + offset).x;
    
            Instantiate(peoplePrefab, spawnPosition, peoplePrefab.transform.rotation);
        }
    
        public void SpawnPeopleContinuous()
        {
            StartCoroutine(SpawnPeopleCo(spawnTime));
        }
    
        public void StopSpawn()
        {
            coroutineIsRunning = false;
        }
        
        public void SpawnPeopleContinuousNew()
        {
            StartCoroutine(SpawnPeopleCoCo(spawnTime));
        }
        
        IEnumerator SpawnPeopleCoCo(float timeBetween)
        {
            while (true)
            {
                SpawnPeople();
                yield return new WaitForSeconds(timeBetween);
            }
        }
    }
}
