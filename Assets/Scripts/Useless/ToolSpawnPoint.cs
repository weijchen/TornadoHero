using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject toolPrefab;
    [SerializeField] int maxAmount = 3;
    [SerializeField] float coolDown = 5.0f;

    private int currAmount = 0;
    private float timer = 0.0f;
    
    void Start()
    {
    }

    private void Update()
    {
        ToolSpawnChecker();
    }

    private void ToolSpawnChecker()
    {
        //if (currAmount < maxAmount)
       // {
        if (timer > coolDown)
        {
            SpawnTool();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
       // }
    }

    void SpawnTool()
    {
        Instantiate(toolPrefab, transform);
        currAmount += 1;
    }

    public void UseTool()
    {
        currAmount -= 1;
    }
}
