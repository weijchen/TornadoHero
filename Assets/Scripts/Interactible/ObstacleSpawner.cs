using System;
using System.Collections;
using System.Collections.Generic;
using Team13.Round1.TornadoHero;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] obstaclePrefabs;
        [SerializeField] private GameObject specialObstacle;
        [SerializeField] float spawnTime = 2.0f;
        [SerializeField] private GameObject obstacleComingEffect;
    
        public bool coroutineIsRunning = true;
    
        IEnumerator SpawnObstacle(float timeBetween)
        {
            if (coroutineIsRunning)
            {
                SpawnObstacle();
                yield return new WaitForSeconds(timeBetween);
            }
        }
        
        IEnumerator SpawnObstacleNew(float timeBetween)
        {
            while (true)
            {
                SpawnObstacle();
                yield return new WaitForSeconds(timeBetween);
            }
        }
        
        public void SpawnObstacle()
        {
            int spawnObstacle = Random.Range(0, 10);
    
            float gameTimer = GameManager.Instance.GetGameTimer();
            obstacleComingEffect.gameObject.SetActive(true);
    
            if (gameTimer < 20)
            {
                Instantiate(specialObstacle, transform);
            }
            else
            {
                if (spawnObstacle >= 5)
                {
                    Instantiate(obstaclePrefabs[0], transform);
                }
                else
                {
                    Instantiate(obstaclePrefabs[1], transform);
                }
            }
        }
        
        public void SpawnObstacleContinuous()
        {
            StartCoroutine(SpawnObstacle(spawnTime));
        }
        
        public void SpawnObstacleContinuousNew()
        {
            StartCoroutine(SpawnObstacleNew(spawnTime));
        }
    
        public void StopSpawn()
        {
            coroutineIsRunning = false;
        }
    }
}
