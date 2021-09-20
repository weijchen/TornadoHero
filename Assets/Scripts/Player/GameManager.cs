using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private bool isTutorialDone = false;
    private int tutorialStep = 11;
    private HandPresence[] _handPresences;
    private float timer = 0;
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;
    private PlayerManager _playerManager;
    private PeopleSpawnPoint[] _peopleSpawnPoint;
    private ObstacleSpawner _obstacleSpawner;
    private int barrelHitInTutorial = 0;
    private float gameTimer;
    private bool obstacleIsSpawning = false;
    private int finalScore = 0;

    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private int saveMultiplier = 1000;
    [SerializeField] private int hitMultiplier = 500;
    [SerializeField] private int comboMultiplier = 2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _peopleSpawnPoint = FindObjectsOfType<PeopleSpawnPoint>();
        _handPresences = FindObjectsOfType<HandPresence>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    private void Update()
    {
        CheckPlayProgress();
        CheckGameFinish();
    }

    private void CheckGameFinish()
    {
        if (gameTimer < 0)
        {
            SceneManager.LoadScene("FinalEndScene");
        }
    }

    public void CheckPlayProgress()
    {
        string currSceneName = GetCurrScene();

        if (currSceneName.Equals("FinalPlayScene"))
        {
            if (!isTutorialDone)
            {
                // Hero... Try save as many people as you can
                if (tutorialStep == 0)
                {
                    instructionPanel.transform.GetChild(0).gameObject.SetActive(true);
                    if (timer > 3)
                    {
                        tutorialStep += 1;
                    }
                    timer += Time.deltaTime;
                } 
                // Press [Right Trigger] to spawn Hook
                else if (tutorialStep == 1)
                {
                    instructionPanel.transform.GetChild(0).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(1).gameObject.SetActive(true);
                    if (rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
                    {
                        tutorialStep += 1;
                    }
                } 
                // Press [Right Grip] to grab Hook
                else if (tutorialStep == 2)
                {
                    instructionPanel.transform.GetChild(1).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(2).gameObject.SetActive(true);
                    if (rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue)
                    {
                        tutorialStep += 1;
                    }
                } 
                // Throw Hook while releasing [Right Grip]
                else if (tutorialStep == 3)
                {
                    instructionPanel.transform.GetChild(2).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(3).gameObject.SetActive(true);
                    if (rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && !gripValue)
                    {
                        timer = 0;
                        tutorialStep += 1;
                    }
                } 
                // Catch the flying people!
                else if (tutorialStep == 4)
                {
                    instructionPanel.transform.GetChild(3).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(4).gameObject.SetActive(true);

                    if (_playerManager.GetSavedAmount() == 1)
                    {
                        timer = 0;
                        tutorialStep += 1;
                    } 
                    else
                    {
                        if (timer > 10)
                        {
                            timer = 0;
                        }
                        else if (timer < 0.1f)
                        {
                            _peopleSpawnPoint[0].SpawnPeople();
                        }
                        timer += Time.deltaTime;
                    }
                } 
                // A barrel is coming!
                else if (tutorialStep == 5)
                {
                    instructionPanel.transform.GetChild(4).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(5).gameObject.SetActive(true);
                    
                    if (timer > 6)
                    {
                        timer = 0;
                        tutorialStep += 1;
                    } else if (timer == 0)
                    {
                        _obstacleSpawner.SpawnObstacle();
                    }
                    timer += Time.deltaTime;
                } 
                // Put both your controllers closer and align them.
                else if (tutorialStep == 6)
                {
                    instructionPanel.transform.GetChild(5).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(6).gameObject.SetActive(true);
                    if (_playerManager.canSpawnBat)
                    {
                        tutorialStep += 1;
                    }
                } 
                // Press [Left Trigger] to spawn Bat
                else if (tutorialStep == 7)
                {
                    instructionPanel.transform.GetChild(6).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(7).gameObject.SetActive(true);
                    if (!_playerManager.canSpawnBat)
                    {
                        tutorialStep -= 1;
                        instructionPanel.transform.GetChild(7).gameObject.SetActive(false);
                    }
                    if (leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
                    {
                        tutorialStep += 1;
                    }
                } 
                // Press [Left/Right Grip] to grab the Bat
                else if (tutorialStep == 8)
                {
                    instructionPanel.transform.GetChild(7).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(8).gameObject.SetActive(true);
                    if (leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripLValue) && gripLValue && rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripRValue) && gripRValue )
                    {
                        tutorialStep += 1;
                        timer = 0;
                    }
                } 
                // Hit the barrel!
                else if (tutorialStep == 9)
                {
                    instructionPanel.transform.GetChild(8).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(9).gameObject.SetActive(true);

                    if (barrelHitInTutorial > 0)
                    {
                        timer = 0;
                        tutorialStep += 1;
                    } 
                    else
                    {
                        if (timer > 10)
                        {
                            _obstacleSpawner.SpawnObstacle();
                            timer = 0;
                        }
                        else if (timer == 0)
                        {
                            _obstacleSpawner.SpawnObstacle();
                        }
                    }
                    timer += Time.deltaTime;
                } 
                // You're all set! Let's start!
                else if (tutorialStep == 10)
                {
                    instructionPanel.transform.GetChild(9).gameObject.SetActive(false);
                    instructionPanel.transform.GetChild(10).gameObject.SetActive(true);
                    timer += Time.deltaTime;
                    if (timer > 3)
                    {
                        tutorialStep += 1;
                        timer = 0;
                    }
                } 
                else
                {
                    foreach (var point in _peopleSpawnPoint)
                    {
                        point.SpawnPeopleContinuous();
                    }

                    _playerManager.InitiateState();
                    instructionPanel.transform.GetChild(10).gameObject.SetActive(false);
                    instructionPanel.gameObject.SetActive(false);
                    isTutorialDone = true;
                    gameTimer = 60;
                }
            }
            else
            {
                if (gameTimer < 30)
                {
                    if (!obstacleIsSpawning)
                    {
                        _obstacleSpawner.SpawnObstacleContinuous();
                    }
                    obstacleIsSpawning = true;
                }
                gameTimer -= Time.deltaTime;
            }
            
        }
    }

    public void HitTutorialBarrel()
    {
        barrelHitInTutorial += 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FinalPlayScene");
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
        return _playerManager.GetSavedAmount() * saveMultiplier;
    }

    public int GetHitScore()
    {
        return _playerManager.GetHitAmount() * hitMultiplier;
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
        else 
        {
            return "D";
        }
    }
}
