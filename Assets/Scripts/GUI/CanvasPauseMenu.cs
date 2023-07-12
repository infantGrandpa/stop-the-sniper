using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

namespace SniperProject {
    public class CanvasPauseMenu : MonoBehaviour
    {
        public static CanvasPauseMenu Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(CanvasPauseMenu)) as CanvasPauseMenu;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static CanvasPauseMenu instance;

        [SerializeField] GameObject resumeButton;
        
        [SerializeField, BoxGroup("Winning")] TMP_Text titleText;
        [SerializeField, BoxGroup("Winning")] string textOnWin;
        [SerializeField, BoxGroup("Winning")] GameObject winBoxParent;
        [SerializeField, BoxGroup("Winning")] TMP_Text escapedValue;
        [SerializeField, BoxGroup("Winning")] TMP_Text lostValue;


        [SerializeField] bool manageAudio = false;

        [SerializeField, ShowIfGroup("manageAudio")] GameObject masterMusicObject;
        private Slider masterMusicSlider;

        [SerializeField, ShowIfGroup("manageAudio")] GameObject masterSFXObject;
        private Slider masterSFXSlider;

        [SerializeField, ShowIfGroup("manageAudio")] GameObject masterVoiceObject;
        private Slider masterVoiceSlider;


        private void Awake()
        {
            if (manageAudio)
            {
                GetUIElements();
                InitUIElements();
            }
        }


        private void GetUIElements()
        {
            if (!manageAudio)
            {
                return;
            }
                masterMusicSlider = masterMusicObject.GetComponent<Slider>();
            if (masterMusicSlider == null)
            {
                Debug.LogError("ERROR CanvasPauseMenu GetUIElements: " + masterMusicObject.name + " does not have a slider component.");
                return;
            }

            masterSFXSlider = masterSFXObject.GetComponent<Slider>();
            if (masterSFXSlider == null)
            {
                Debug.LogError("ERROR CanvasPauseMenu GetUIElements: " + masterSFXObject.name + " does not have a slider component.");
                return;
            }

            masterVoiceSlider = masterVoiceObject.GetComponent<Slider>();
            if (masterVoiceSlider == null)
            {
                Debug.LogError("ERROR CanvasPauseMenu GetUIElements: " + masterVoiceObject.name + " does not have a slider component.");
                return;
            }
        }

        public void ChangeMasterMusicVolume()
        {
            if (!manageAudio)
            {
                return;
            }
            AudioManager.AudioManager.Instance.ChangeMasterMusicVolume(masterMusicSlider.value);
        }

        public void ChangeMasterSFXVolume()
        {
            if (!manageAudio)
            {
                return;
            }
            AudioManager.AudioManager.Instance.ChangeMasterSFXVolume(masterSFXSlider.value);
        }

        public void ChangeMasterVoiceVolume()
        {
            if (!manageAudio)
            {
                return;
            }
            AudioManager.AudioManager.Instance.ChangeMasterVoiceVolume(masterVoiceSlider.value);
        }

        public void ToggleMusicMute()
        {
            if (!manageAudio)
            {
                return;
            }

            if (AudioManager.AudioManager.Instance.masterMusicVolume == 0)
            {
                AudioManager.AudioManager.Instance.ChangeMasterMusicVolume(1);
            }
            else
            {
                AudioManager.AudioManager.Instance.ChangeMasterMusicVolume(0);
            }

            masterMusicSlider.value = AudioManager.AudioManager.Instance.masterMusicVolume;
        }

        public void ToggleSFXMute()
        {
            if (!manageAudio)
            {
                return;
            }
            if (AudioManager.AudioManager.Instance.masterSFXVolume == 0)
            {
                AudioManager.AudioManager.Instance.ChangeMasterSFXVolume(1);
            }
            else
            {
                AudioManager.AudioManager.Instance.ChangeMasterSFXVolume(0);
            }

            masterSFXSlider.value = AudioManager.AudioManager.Instance.masterSFXVolume;
        }

        public void ToggleVoiceMute()
        {
            if (!manageAudio)
            {
                return;
            }
            if (AudioManager.AudioManager.Instance.masterVoiceVolume == 0)
            {
                AudioManager.AudioManager.Instance.ChangeMasterVoiceVolume(1);
            }
            else
            {
                AudioManager.AudioManager.Instance.ChangeMasterVoiceVolume(0);
            }

            masterVoiceSlider.value = AudioManager.AudioManager.Instance.masterVoiceVolume;
        }

        private void InitUIElements()
        {
            if (!manageAudio)
            {
                return;
            }
            masterMusicSlider.value = AudioManager.AudioManager.Instance.masterMusicVolume;
        }

        public void ResumeGame()
        {
            LevelManager.Instance.UnpauseGame();
            Destroy(gameObject);
        }

        public void ShowCanvas()
        {
            gameObject.SetActive(true);
        }

        public void HideCanvas()
        {
            gameObject.SetActive(false);
        }

        public void ShowWinScreen(int shipsSaved, int shipsLost)
        {
            titleText.text = textOnWin;
            escapedValue.text = shipsSaved.ToString();
            lostValue.text = shipsLost.ToString();
            resumeButton.SetActive(false);
            winBoxParent.SetActive(true);

            LevelManager.Instance.PauseGame();
        }
    }
}