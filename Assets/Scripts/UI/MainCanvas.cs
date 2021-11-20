using System.Collections;
using System.Collections.Generic;
using Team13.Round1.TornadoHero;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

namespace Team13.Round1.TornadoHero
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text savedAmountText;
        [SerializeField] private TMP_Text deadAmountText;
        [SerializeField] private TMP_Text gameTimerCountdown;

        void Update()
        {
            savedAmountText.text = PlayerManager.Instance.GetSavedAmount().ToString();
            deadAmountText.text = PlayerManager.Instance.GetDeadAmount().ToString();
            string timerString = GameManager.Instance.GetGameTimer().ToString();
            if (timerString.Length >= 5)
            {
                gameTimerCountdown.text = timerString.Substring(0, 5);
            }
            else
            {
                gameTimerCountdown.text = timerString;
            }
        }
    }
}
