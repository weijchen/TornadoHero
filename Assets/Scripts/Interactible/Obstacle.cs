using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Team13.Round1.TornadoHero;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

namespace Team13.Round1.TornadoHero
{
    public class Obstacle : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private List<Transform> pickedPath;
        private int pointIndex;
        private XRRig playerPosition;
        private bool isHitted = false;
        private PlayerManager _playerManager;

        [SerializeField] private float hitMultiplier = 2.0f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private ObstacleFlyingPath[] _obstacleFlyingPaths;
        [SerializeField] private float speed = 2.0f;
        [SerializeField] private float arrivedDistance = 0.5f;
        [SerializeField] private float destroyTime = 0.2f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            playerPosition = FindObjectOfType<XRRig>();
            _obstacleFlyingPaths = FindObjectsOfType<ObstacleFlyingPath>();
            PickPath();
        }

        private void Update()
        {
            if (!isHitted)
            {
                Move();
            }
        }

        private void PickPath()
        {
            int pathIndex = Random.Range(0, _obstacleFlyingPaths.Length);
            pickedPath = _obstacleFlyingPaths[pathIndex].GetPathPoints();
        }

        private void Move()
        {
            if (Vector3.Distance(transform.position, playerPosition.transform.position) <= arrivedDistance)
            {
                Destroy(gameObject, destroyTime);
            }
            else
            {
                transform.Rotate(new Vector3(1.0f, 1.0f, 1.0f));
            }

            if (pointIndex <= pickedPath.Count - 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, pickedPath[pointIndex].position,
                    speed * Time.deltaTime);
                if (transform.position == pickedPath[pointIndex].position)
                {
                    pointIndex += 1;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition.transform.position,
                    speed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "Bat")
            {
                isHitted = true;
                _rigidbody.velocity = other.transform.GetComponent<Rigidbody>().velocity * hitMultiplier;
                GameManager.Instance.HitTutorialBarrel();
                _playerManager.obstacleComingEffect.gameObject.SetActive(false); 
                _playerManager.AddHitAmount();
                Destroy(gameObject, 5.0f);
            }

            if ((groundLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                _audioSource.Play();
            }
            _playerManager.obstacleComingEffect.gameObject.SetActive(false);
        }
    }
}
