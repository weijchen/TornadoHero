using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class PeopleSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject peoplePrefab;
    [SerializeField] float maxXOffset = 10.0f;
    [SerializeField] float spawnTime = 2.0f;

    private PlayerManager _playerManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    IEnumerator SpawnPeople(float timeBetween)
    {
        while (true)
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
        StartCoroutine(SpawnPeople(spawnTime));
    }
}
