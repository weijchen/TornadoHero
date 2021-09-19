using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Text savedAmountText;
    [SerializeField] private Text deadAmountText;

    private PlayerManager _playerManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        savedAmountText.text = _playerManager.GetSavedAmount().ToString();
        deadAmountText.text = _playerManager.GetDeadAmount().ToString();
    }
}
