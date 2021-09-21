using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text saveScoreText;
    [SerializeField] private TMP_Text hitScoreText;
    [SerializeField] private TMP_Text comboScoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private GameObject scrollObject;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float scrollSpeed = 5.0f; 

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Time.timeScale = 1;
    }

    void Update()
    {
        saveScoreText.text = _gameManager.GetSaveScore().ToString();
        hitScoreText.text = _gameManager.GetHitScore().ToString();
        comboScoreText.text = _gameManager.GetComboScore().ToString();
        finalScoreText.text = _gameManager.GetFinalScore().ToString();
        rankText.text = _gameManager.GetRank();
        ScoreBoardMoveToPosition();
    }

    public void RestartButtonOnClick()
    {
        _gameManager.RestartGame();
        SceneManager.LoadScene("FinalPlayScene");
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }

    public void ScoreBoardMoveToPosition()
    {
        // scrollObject.transform.position = Vector3.Lerp(scrollObject.transform.position, target.position + (scrollObject.transform.position - target.position).normalized * 10, Time.deltaTime * 5);
        scrollObject.transform.position = Vector3.MoveTowards(scrollObject.transform.position, targetPosition.position, scrollSpeed * Time.deltaTime);
    }
}
