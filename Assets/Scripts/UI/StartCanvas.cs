using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team13.Round1.TornadoHero
{
    public class StartCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject[] imageList;
        [SerializeField] private GameObject startButton;

        private float timer;
        private bool newsStartPlayed = false;
        private bool tornadoPlayed = false;
        private bool dingPlayed = false;
        private bool oneStepPlayed = false;
        private bool screamOnePlayed = false;
        private bool screamTwoPlayed = false;
        private bool showEquipOnePlayed = false;
        private bool showEquipTwoPlayed = false;
        private bool stepsPlayed = false;
        private bool tornadoEffectPlayedFirst = false;
        private bool tornadoEffectPlayedSecond = false;
        private bool bgmPlayed = false;
        
        void Start()
        {
            timer = 0;
        }

        void Update()
        {
            timer += Time.deltaTime;
            ImageRoller();
        }

        private void ImageRoller()
        {
            // image 1
            if (timer > 0.5f)
            {
                if (!newsStartPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.Intro);
                    newsStartPlayed = true;
                }
                OpenImageOnIndex(0);
            }

            if (timer > 2.0f)
            {
                if (!tornadoPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.News);
                    tornadoPlayed = true;
                }
            }
            
            // image 2
            if (timer >= 9.0f)
            {
                if (!dingPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.Ding);
                    dingPlayed = true;
                }
                CloseImageOnIndex(0);
                OpenImageOnIndex(1);
            }

            // image 3
            if (timer >= 16.0f)
            {
                if (!stepsPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.ManySteps);
                    stepsPlayed = true;
                }
                if (!screamOnePlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.ScreamOne, 0.8f);
                    screamOnePlayed = true;
                }
                if (!screamTwoPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.ScreamTwo, 0.8f);
                    screamTwoPlayed = true;
                }
                CloseImageOnIndex(1);
                OpenImageOnIndex(2);
            }

            // image 4-1
            if (timer >= 18.0f)
            {
                if (!showEquipOnePlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.EquipmentOne);
                    showEquipOnePlayed = true;
                }
                CloseImageOnIndex(2);
                OpenImageOnIndex(3);
            }

            // image 4-2 
            if (timer >= 19.0f)
            {
                if (!showEquipTwoPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.EquipmentTwo);
                    showEquipTwoPlayed = true;
                }
                OpenImageOnIndex(4);
            }
            
            // image 5 
            if (timer >= 21.0f)
            {
                if (!oneStepPlayed)
                {
                    SoundManager.Instance.PlayVO(VOIndex.OneStep);
                    oneStepPlayed = true;
                }
                if (!tornadoEffectPlayedFirst)
                {
                    SoundManager.Instance.PlaySFX(SFXIndex.Tornado, 0.5f);
                    tornadoEffectPlayedFirst = true;
                }
                CloseImageOnIndex(3);
                CloseImageOnIndex(4);
                OpenImageOnIndex(5);
            }
            
            // image 6 
            if (timer >= 24.0f)
            {
                if (!tornadoEffectPlayedSecond)
                {
                    SoundManager.Instance.PlaySFX(SFXIndex.Tornado);
                    tornadoEffectPlayedSecond = true;
                }
                if (!bgmPlayed)
                {
                    SoundManager.Instance.PlayBGM(BGMIndex.Main);
                    bgmPlayed = true;
                }
                CloseImageOnIndex(5);
                OpenImageOnIndex(6);
            }
            
            // start button 
            if (timer >= 27.0f)
            {
                startButton.SetActive(true);
            }
            
        }

        private void OpenImageOnIndex(int index)
        {
            imageList[index].SetActive(true);
        }
        
        private void CloseImageOnIndex(int index)
        {
            imageList[index].SetActive(false);
        }
        
        public void StartGame()
        {
            SceneManager.LoadScene(SceneCategory.Main);
        }
    }
}
