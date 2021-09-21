using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject specialObstacle;
    [SerializeField] float spawnTime = 2.0f;
    [SerializeField] private GameObject obstacleComingEffect;

    public bool coroutineIsRunning = true;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    IEnumerator SpawnObstacle(float timeBetween)
    {
        if (coroutineIsRunning)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(timeBetween);
        }
    }
    
    public void SpawnObstacle()
    {
        int spawnObstacle = Random.Range(0, 1);

        float gameTimer = _gameManager.GetGameTimer();
        obstacleComingEffect.gameObject.SetActive(true);

        if (gameTimer < 15)
        {
            Instantiate(specialObstacle, transform);
        }
        else
        {
            Instantiate(obstaclePrefabs[spawnObstacle], transform);
        }
    }
    
    public void SpawnObstacleContinuous()
    {
        StartCoroutine(SpawnObstacle(spawnTime));
    }

    public void StopSpawn()
    {
        coroutineIsRunning = false;
    }
}
