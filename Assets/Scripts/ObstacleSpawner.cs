using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float spawnTime = 2.0f;

    IEnumerator SpawnObstacle(float timeBetween)
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(timeBetween);
        }
    }
    
    public void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, transform);
    }
    
    public void SpawnObstacleContinuous()
    {
        StartCoroutine(SpawnObstacle(spawnTime));
    }
}
