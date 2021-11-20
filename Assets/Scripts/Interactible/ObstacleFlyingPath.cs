using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team13.Round1.TornadoHero
{
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
}
