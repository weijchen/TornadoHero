using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] private Text saveScoreText;
    [SerializeField] private Text hitScoreText;
    [SerializeField] private Text comboScoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text rankText;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        saveScoreText.text = _gameManager.GetSaveScore().ToString();
        hitScoreText.text = _gameManager.GetHitScore().ToString();
        comboScoreText.text = _gameManager.GetComboScore().ToString();
        finalScoreText.text = _gameManager.GetFinalScore().ToString();
        rankText.text = _gameManager.GetRank();
    }
}
