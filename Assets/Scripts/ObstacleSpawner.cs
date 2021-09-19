using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float spawnTime = 2.0f;

    private bool isSpawning;
    
    void Start()
    {
        isSpawning = false;
        StartCoroutine(SpawnObstacle(spawnTime));
    }

    IEnumerator SpawnObstacle(float timeBetween)
    {
        while (isSpawning)
        {
            Instantiate(obstaclePrefab, transform);
            yield return new WaitForSeconds(timeBetween);
        }
    }
    
    public void SetIsSpawn(bool state)
    {
        isSpawning = state;
    }
}
