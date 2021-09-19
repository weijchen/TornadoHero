using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleFlyingPath : MonoBehaviour
{
    [SerializeField] List<Transform> pathPoints;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            pathPoints.Add(child);
        }
    }

    public List<Transform> GetPathPoints()
    {
        return pathPoints;
    }
}
