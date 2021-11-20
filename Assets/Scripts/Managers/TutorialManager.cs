using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Team13.Round1.TornadoHero
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance = null;

        [SerializeField] public GameObject instructionPanel = null;
        
        private bool isTutorialDone = false;
        private int tutorialStep = 0;
        private float timer = 0;
        private bool obstacleIsSpawning = false;
        private bool skyPeopleIsSpawning = false;
        private bool groundPeopleIsSpawning = false;
        private int barrelHitInTutorial = 0;

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

        public void RestartGame()
        {
            obstacleIsSpawning = false;
            skyPeopleIsSpawning = false;
            groundPeopleIsSpawning = false;
        }

        public void HitTutorialBarrel()
        {
            barrelHitInTutorial += 1;
        }

        public void CheckPlayProgress()
        {
            string currSceneName = GameManager.Instance.GetCurrScene();

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
                        if (GameManager.Instance.rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) &&
                            triggerValue)
                        {
                            tutorialStep += 1;
                        }
                    }
                    // Press [Right Grip] to grab Hook
                    else if (tutorialStep == 2)
                    {
                        instructionPanel.transform.GetChild(1).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(2).gameObject.SetActive(true);
                        if (GameManager.Instance.rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) &&
                            gripValue)
                        {
                            tutorialStep += 1;
                        }
                    }
                    // Throw Hook while releasing [Right Grip]
                    else if (tutorialStep == 3)
                    {
                        instructionPanel.transform.GetChild(2).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(3).gameObject.SetActive(true);
                        if (GameManager.Instance.rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) &&
                            !gripValue)
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

                        if (PlayerManager.Instance.GetSavedAmount() == 1)
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
                                GameManager.Instance._skyPeopleSpawnPoint[0].SpawnPeople();
                            }

                            timer += Time.deltaTime;
                        }
                    }
                    // A barrel is coming!
                    //If hit, it'll stun your grabbing system for three seconds. Be careful!
                    else if (tutorialStep == 5)
                    {
                        instructionPanel.transform.GetChild(4).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(5).gameObject.SetActive(true);

                        if (timer > 6)
                        {
                            timer = 0;
                            tutorialStep += 1;
                        }
                        else if (timer > 3)
                        {
                            instructionPanel.transform.GetChild(5).gameObject.SetActive(false);
                            instructionPanel.transform.GetChild(6).gameObject.SetActive(true);
                        }
                        else if (timer == 0)
                        {
                            GameManager.Instance._obstacleSpawner.SpawnObstacle();
                        }

                        timer += Time.deltaTime;
                    }
                    // Put both your controllers closer and align them.
                    else if (tutorialStep == 6)
                    {
                        instructionPanel.transform.GetChild(5).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(6).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(7).gameObject.SetActive(true);
                        if (PlayerManager.Instance.canSpawnBat)
                        {
                            tutorialStep += 1;
                        }
                    }
                    // Press [Left Trigger] to spawn Bat
                    else if (tutorialStep == 7)
                    {
                        instructionPanel.transform.GetChild(7).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(8).gameObject.SetActive(true);
                        if (!PlayerManager.Instance.canSpawnBat)
                        {
                            tutorialStep -= 1;
                            instructionPanel.transform.GetChild(8).gameObject.SetActive(false);
                        }

                        if (GameManager.Instance.leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) &&
                            triggerValue)
                        {
                            tutorialStep += 1;
                        }
                    }
                    // Press [Left/Right Grip] to grab the Bat
                    else if (tutorialStep == 8)
                    {
                        instructionPanel.transform.GetChild(8).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(9).gameObject.SetActive(true);
                        if (GameManager.Instance.leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripLValue) &&
                            gripLValue &&
                            GameManager.Instance.rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripRValue) &&
                            gripRValue)
                        {
                            tutorialStep += 1;
                            timer = 0;
                        }
                    }
                    // Hit the barrel!
                    else if (tutorialStep == 9)
                    {
                        instructionPanel.transform.GetChild(9).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(10).gameObject.SetActive(true);

                        if (barrelHitInTutorial > 0)
                        {
                            timer = 0;
                            tutorialStep += 1;
                        }
                        else
                        {
                            if (timer > 10)
                            {
                                GameManager.Instance._obstacleSpawner.SpawnObstacle();
                                timer = 0;
                            }
                            else if (timer == 0)
                            {
                                GameManager.Instance._obstacleSpawner.SpawnObstacle();
                            }
                        }

                        timer += Time.deltaTime;
                    }
                    // You're all set! Let's start!
                    else if (tutorialStep == 10)
                    {
                        instructionPanel.transform.GetChild(10).gameObject.SetActive(false);
                        instructionPanel.transform.GetChild(11).gameObject.SetActive(true);
                        timer += Time.deltaTime;
                        if (timer > 3)
                        {
                            tutorialStep += 1;
                            timer = 0;
                        }
                    }
                    else
                    {
                        foreach (var point in GameManager.Instance._groundPeopleSpawnPoint)
                        {
                            point.SpawnPeopleContinuous();
                        }

                        if (!GameManager.Instance.hasReinitiate)
                        {
                            PlayerManager.Instance.InitiateState();
                            instructionPanel.transform.GetChild(11).gameObject.SetActive(false);
                            instructionPanel.gameObject.SetActive(false);
                            isTutorialDone = true;
                            GameManager.Instance.gameTimer = GameManager.Instance.gameTime;
                            GameManager.Instance.hasReinitiate = true;
                        }
                    }
                }
                else
                {
                    // sky people spawning
                    if (!skyPeopleIsSpawning)
                    {
                        foreach (var point in GameManager.Instance._skyPeopleSpawnPoint)
                        {
                            point.SpawnPeopleContinuousNew();
                        }
                    }

                    skyPeopleIsSpawning = true;

                    // obstacles spawning
                    if (GameManager.Instance.gameTimer < GameManager.Instance.startSpawnObstacle)
                    {
                        if (!obstacleIsSpawning)
                        {
                            GameManager.Instance._obstacleSpawner.SpawnObstacleContinuousNew();
                        }

                        obstacleIsSpawning = true;
                    }

                    // ground people spawning
                    if (GameManager.Instance.gameTimer < GameManager.Instance.startSpawnGround)
                    {
                        if (!groundPeopleIsSpawning)
                        {
                            foreach (var point in GameManager.Instance._groundPeopleSpawnPoint)
                            {
                                point.SpawnPeopleContinuousNew();
                            }
                        }

                        groundPeopleIsSpawning = true;
                    }

                    if (GameManager.Instance.gameTimer > 0)
                    {
                        GameManager.Instance.gameTimer -= Time.deltaTime;
                    }
                    else
                    {
                        GameManager.Instance.gameTimer = 0f;
                    }
                }
            }
        }
    }

}
