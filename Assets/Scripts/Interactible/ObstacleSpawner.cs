using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
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
    
    public void SpawnObstacle()
    {
        obstacleComingEffect.gameObject.SetActive(true);
        Instantiate(obstaclePrefab, transform);
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
