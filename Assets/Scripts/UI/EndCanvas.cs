using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Team13.Round1.TornadoHero
{
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
        [SerializeField] private GameObject creditCube;

        private void Start()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
            saveScoreText.text = GameManager.Instance.GetSaveScore().ToString();
            hitScoreText.text = GameManager.Instance.GetHitScore().ToString();
            comboScoreText.text = GameManager.Instance.GetComboScore().ToString();
            finalScoreText.text = GameManager.Instance.GetFinalScore().ToString();
            rankText.text = GameManager.Instance.GetRank();
            ScoreBoardMoveToPosition();
        }

        public void CreditButtonOnClick()
        {
            creditCube.SetActive(true);
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
}