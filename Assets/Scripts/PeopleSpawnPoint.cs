using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PeopleSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject peoplePrefab;
    [SerializeField] float maxXOffset = 10.0f;
    [SerializeField] float spawnTime = 2.0f;
    [SerializeField] float destroyTime = 20.0f;

    private PlayerManager _playerManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        StartCoroutine(SpawnPeople(spawnTime));
    }

    IEnumerator SpawnPeople(float timeBetween)
    {
        while (true)
        {
            Vector3 offset = Vector3.zero;
            float randomXOffset = Random.Range(-maxXOffset, maxXOffset);
            offset += new Vector3(randomXOffset, 0, 0);
            
            Vector3 spawnPosition = transform.position;
            spawnPosition.x = (transform.position + offset).x;
            
            GameObject people = Instantiate(peoplePrefab, spawnPosition, Quaternion.identity);
            Invoke("DestroyPrefab", destroyTime);
            Destroy(people, destroyTime);
            yield return new WaitForSeconds(timeBetween);
        }
    }

    private void DestroyPrefab()
    {
        _playerManager.AddDeadAmount();
    }
}
