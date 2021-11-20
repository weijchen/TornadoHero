using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Team13.Round1.TornadoHero;
using UnityEngine;
using UnityEngine.XR;

namespace Team13.Round1.TornadoHero
{
    public class HandPresence : MonoBehaviour
    {
        [SerializeField] bool showController = false;
        [SerializeField] InputDeviceCharacteristics controllerCharacteristics;
        [SerializeField] List<GameObject> controllerPrefabs;
        [SerializeField] GameObject handModelPrefab;
        [SerializeField] GameObject hookPrefab;
        [SerializeField] GameObject batPrefab;

        private Vector3 targetDevicePosition;
        private InputDevice targetDevice;
        private InputDevice targetLeftHandDevice;
        private GameObject spawnedController;
        private GameObject spawnedHandModel;
        private Animator handAnimator;
        private bool UseBat = false;
        private bool UsingBat = false;
        private bool hasSpawnBat = false;
        private bool hasSpawnHook = false;
        private bool isBat = false;

        public InputDevice leftDevice;
        public InputDevice rightDevice;
        private List<XRNodeState> _nodeStates = new List<XRNodeState>();
        private Vector3 leftHandPosition;
        private Vector3 rightHandPosition;
        private float batAngle = 30.0f;
        private float batDistance = 0.3f;
        private string currScene = "";

        void Start()
        {
            TryInitialize();
        }
        
        void Update()
        {
            if (!FindObjectOfType<StartCanvas>())
            {
                currScene = GameManager.Instance.GetCurrScene();
            
                if (!targetDevice.isValid)
                {
                    TryInitialize();
                }
                else
                {
                    UpdateDevicePosition();
                    if (showController)
                    {
                        spawnedController.SetActive(true);
                        spawnedHandModel.SetActive(false);
                    }
                    else
                    {
                        spawnedController.SetActive(false);
                        spawnedHandModel.SetActive(true);

                        if (currScene.Equals(SceneCategory.Main.ToString()))
                        {
                            CheckHookSpawn();
                            CheckBatRequirement();
                            if (isBat)
                            {
                                CheckIsBat();
                            }
                        }
                        UpdateHandAnimation();
                    }
                }
            }
        }
        
        private void CheckBatRequirement()
        {
            InputTracking.GetNodeStates(_nodeStates);
            var leftHandState = _nodeStates.FirstOrDefault(node => node.nodeType == XRNode.LeftHand);
            var rightHandState = _nodeStates.FirstOrDefault(node => node.nodeType == XRNode.RightHand);

            leftHandState.TryGetPosition(out leftHandPosition);
            rightHandState.TryGetPosition(out rightHandPosition);
            
            float angle = Vector3.Angle(leftHandPosition, rightHandPosition);
            float distance = Vector3.Distance(leftHandPosition, rightHandPosition);

            if (angle <= batAngle && distance <= batDistance)
            {
                isBat = true;
                PlayerManager.Instance.canSpawnBat = true;
            }
            else
            {
                isBat = false;
                PlayerManager.Instance.canSpawnBat = false;
            }
        }

        private void CheckIsBat()
        {
            bool triggerValue;
            if (leftDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (!hasSpawnBat)
                {
                    InputTracking.GetNodeStates(_nodeStates);
                    var leftHandState = _nodeStates.FirstOrDefault(node => node.nodeType == XRNode.LeftHand);
                    leftHandState.TryGetPosition(out leftHandPosition);
                    Instantiate(batPrefab, leftHandPosition + new Vector3(0, 1.0f, 0), Quaternion.identity);
                    hasSpawnBat = true;
                }
            }
            else
            {
                hasSpawnBat = false;
            }
        }
        
        private void CheckHookSpawn()
        {
            bool triggerValue;
            if (rightDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (!hasSpawnHook)
                {
                    InputTracking.GetNodeStates(_nodeStates);
                    var rightHandState = _nodeStates.FirstOrDefault(node => node.nodeType == XRNode.RightHand);
                    rightHandState.TryGetPosition(out rightHandPosition);
                    Instantiate(hookPrefab, rightHandPosition + new Vector3(0, 1.0f, 0), Quaternion.identity);
                    hasSpawnHook = true;
                }
            }
            else
            {
                hasSpawnHook = false;
            }
        }

        void TryInitialize()
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

            if (devices.Count > 0)
            {
                targetDevice = devices[0];
                GameObject prefab;
                
                foreach (var device in devices)
                {
                    if (device.name.Contains("Right"))
                    {
                        rightDevice = device;
                        GameManager.Instance.SetRightHandDevice(device);
                    }
                    else
                    {
                        leftDevice = device;
                        GameManager.Instance.SetLeftHandDevice(device);
                    }
                }
                
                prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

                if (prefab)
                {
                    spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.LogError("Did not find corresponding controller model");
                    spawnedController = Instantiate(controllerPrefabs[0], transform);
                }
                spawnedHandModel = Instantiate(handModelPrefab, transform);
                handAnimator = spawnedHandModel.GetComponent<Animator>();    
            } 
        }

        void UpdateDevicePosition()
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 positionValue))
            {
                targetDevicePosition = positionValue;
            }
            else
            {
                targetDevicePosition = Vector3.zero;
            }

        }

        public Vector3 getTargetDevicePosition()
        {
            return targetDevicePosition;
        }

        void UpdateHandAnimation()
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                handAnimator.SetFloat("Trigger", triggerValue);
            }
            else
            {
                handAnimator.SetFloat("Trigger", 0);
            }
            
            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                handAnimator.SetFloat("Grip", gripValue);
            }
            else
            {
                handAnimator.SetFloat("Grip", 0);
            }
        }
    }
}
