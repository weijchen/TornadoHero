using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team13.Round1.TornadoHero
{
    public class StartCanvas : MonoBehaviour
    {
        private float timer;
        private AudioSource _audioSource;
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

        [SerializeField] private GameObject[] imageList;
        [SerializeField] private GameObject startButton;
        [SerializeField] private AudioClip _newsStartClip;
        [SerializeField] private AudioClip _tornadoClip;
        [SerializeField] private AudioClip _dingClip;
        [SerializeField] private AudioClip _oneStepClip;
        [SerializeField] private AudioClip _screamOneClip;
        [SerializeField] private AudioClip _screamTwoClip;
        [SerializeField] private AudioClip _showEquipOneClip;
        [SerializeField] private AudioClip _showEquipTwoClip;
        [SerializeField] private AudioClip _stepsClip;
        [SerializeField] private AudioClip _tornadoEffectClip;
        [SerializeField] private AudioClip _bgmClip;
        
        void Start()
        {
            timer = 0;
            _audioSource = GetComponent<AudioSource>();
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
                    _audioSource.PlayOneShot(_newsStartClip);
                    newsStartPlayed = true;
                }
                OpenImageOnIndex(0);
            }

            if (timer > 2.0f)
            {
                if (!tornadoPlayed)
                {
                    _audioSource.PlayOneShot(_tornadoClip);
                    tornadoPlayed = true;
                }
            }
            
            // image 2
            if (timer >= 9.0f)
            {
                if (!dingPlayed)
                {
                    _audioSource.PlayOneShot(_dingClip);
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
                    _audioSource.PlayOneShot(_stepsClip);
                    stepsPlayed = true;
                }
                if (!screamOnePlayed)
                {
                    _audioSource.PlayOneShot(_screamOneClip, 0.8f);
                    screamOnePlayed = true;
                }
                if (!screamTwoPlayed)
                {
                    _audioSource.PlayOneShot(_screamTwoClip, 0.8f);
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
                    _audioSource.PlayOneShot(_showEquipOneClip);
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
                    _audioSource.PlayOneShot(_showEquipTwoClip);
                    showEquipTwoPlayed = true;
                }
                OpenImageOnIndex(4);
            }
            
            // image 5 
            if (timer >= 21.0f)
            {
                if (!oneStepPlayed)
                {
                    _audioSource.PlayOneShot(_oneStepClip);
                    oneStepPlayed = true;
                }
                if (!tornadoEffectPlayedFirst)
                {
                    _audioSource.PlayOneShot(_tornadoEffectClip, 0.5f);
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
                    _audioSource.PlayOneShot(_tornadoEffectClip);
                    tornadoEffectPlayedSecond = true;
                }
                if (!bgmPlayed)
                {
                    _audioSource.PlayOneShot(_bgmClip);
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
            SceneManager.LoadScene("FinalPlayScene");
        }
    }
}
