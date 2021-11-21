using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace Team13.Round1.TornadoHero
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        [Header("General")]
        [SerializeField] public int gameTime = 80;
        [SerializeField] public int startSpawnGround = 60;
        [SerializeField] public int startSpawnObstacle = 40;
        [SerializeField] public int startSpawnETC = 20;
        [SerializeField] private int saveMultiplier = 1000;
        [SerializeField] private int hitMultiplier = 500;
        [SerializeField] private int comboMultiplier = 2;

        [Header("Tornado")]
        [SerializeField] private GameObject tornadoObject;
        [SerializeField] private Transform tornadoLeavePoint;
        [SerializeField] private float tornadoLeaveSpeed = 7.5f;
        
        [SerializeField] public PeopleSpawnPoint[] _skyPeopleSpawnPoint;
        [SerializeField] public PeopleSpawnPoint[] _groundPeopleSpawnPoint;
        private bool obstacleIsSpawning = false;
        private bool skyPeopleIsSpawning = false;
        private bool groundPeopleIsSpawning = false;
        
        public InputDevice leftHandDevice;
        public InputDevice rightHandDevice;
        public bool isTutorialDone = false;
        
        public ObstacleSpawner _obstacleSpawner;

        public float gameTimer = 120.0f;
        public bool hasReinitiate = false;

        private float timer = 0;
        private int finalScore = 0;
        private float delayTimer = 0f;
        private float delayTimeToFinalScene = 5.0f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        }

        private void Update()
        {
            string currSceneName = GetCurrScene();

            if (currSceneName.Equals(SceneCategory.Main))
            {
                if (!isTutorialDone)
                {
                    TutorialManager.Instance.CheckPlayProgress();
                }
                else
                {
                    CheckPlayProgress();
                }
            }

            CheckGameFinish();
        }

        private void CheckPlayProgress()
        {
            // sky people spawning
            if (!skyPeopleIsSpawning)
            {
                foreach (var point in _skyPeopleSpawnPoint)
                {
                    point.SpawnPeopleContinuousNew();
                }
            }

            skyPeopleIsSpawning = true;

            // obstacles spawning
            if (gameTimer < startSpawnObstacle)
            {
                if (!obstacleIsSpawning)
                {
                    _obstacleSpawner.SpawnObstacleContinuousNew();
                }

                obstacleIsSpawning = true;
            }

            // ground people spawning
            if (gameTimer < startSpawnGround)
            {
                if (!groundPeopleIsSpawning)
                {
                    foreach (var point in _groundPeopleSpawnPoint)
                    {
                        point.SpawnPeopleContinuousNew();
                    }
                }

                groundPeopleIsSpawning = true;
            }

            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            {
                gameTimer = 0f;
            }
        }
        public void RestartGame()
        {
            gameTimer = gameTime;
            obstacleIsSpawning = false;
            skyPeopleIsSpawning = false;
            groundPeopleIsSpawning = false;
        }

        private void CheckGameFinish()
        {
            if (gameTimer == 0)
            {
                _obstacleSpawner.StopSpawn();
                foreach (var point in _groundPeopleSpawnPoint)
                {
                    point.StopSpawn();
                }

                foreach (var point in _skyPeopleSpawnPoint)
                {
                    point.StopSpawn();
                }

                delayTimer += Time.deltaTime;

                tornadoObject.transform.position = Vector3.MoveTowards(tornadoObject.transform.position,
                    tornadoLeavePoint.position, tornadoLeaveSpeed * Time.deltaTime);

                if (delayTimer > delayTimeToFinalScene)
                {
                    SceneManager.LoadScene(SceneCategory.End);
                }
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneCategory.Main);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public string GetCurrScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        public void SetLeftHandDevice(InputDevice device)
        {
            leftHandDevice = device;
        }

        public void SetRightHandDevice(InputDevice device)
        {
            rightHandDevice = device;
        }

        public float GetGameTimer()
        {
            return gameTimer;
        }

        public int GetSaveScore()
        {
            return PlayerManager.Instance.GetSavedAmount() * saveMultiplier;
        }

        public int GetHitScore()
        {
            return PlayerManager.Instance.GetHitAmount() * hitMultiplier;
        }

        public int GetComboScore()
        {
            return GetSaveScore() * comboMultiplier;
        }

        public int GetFinalScore()
        {
            finalScore = GetSaveScore() + GetHitScore() + GetComboScore();
            return finalScore;
        }

        public string GetRank()
        {
            int finalScore = GetFinalScore();

            if (finalScore >= 10000)
            {
                return "A";
            }

            if (finalScore < 10000 && finalScore >= 5000)
            {
                return "B";
            }

            if (finalScore < 5000 && finalScore >= 2500)
            {
                return "C";
            }

            return "D";
        }
    }
}