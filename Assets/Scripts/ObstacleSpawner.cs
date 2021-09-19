using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float spawnTime = 2.0f;
    
    void Start()
    {
        StartCoroutine(SpawnObstacle(spawnTime));
    }

    IEnumerator SpawnObstacle(float timeBetween)
    {
        while (true)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform);
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
