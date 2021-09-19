using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Text savedAmountText;
    [SerializeField] private Text deadAmountText;
    [SerializeField] private Text gameTimerCountdown;

    private PlayerManager _playerManager;
    private GameManager _gameManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        savedAmountText.text = _playerManager.GetSavedAmount().ToString();
        deadAmountText.text = _playerManager.GetDeadAmount().ToString();
        gameTimerCountdown.text = _gameManager.GetGameTimer().ToString();
    }
}
