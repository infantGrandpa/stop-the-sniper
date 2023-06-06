using System.Collections.Generic;
using UnityEngine;

namespace AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        #region Properties and Variables

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static AudioManager _instance;

        [SerializeField] SoundScriptableObject[] soundsToLoop;
        public Transform defaultParentTransform;
        public Transform mainCameraTransform;

        public float masterSFXVolume { get; private set; }
        public float masterMusicVolume { get; private set; }
        public float masterVoiceVolume { get; private set; }

        public List<AudioPlayer> sfxPlayers = new();
        public List<AudioPlayer> musicPlayers = new();
        public List<AudioPlayer> voicePlayers = new();

        #endregion

        #region Spawning and Destroying

        private void Awake()
        {
            DontDestroyOnLoad(transform.parent);

            if (mainCameraTransform == null)
            {
                mainCameraTransform = Camera.main.transform;
            }
        }


        private void OnEnable()
        {
            masterSFXVolume = 1;
            masterMusicVolume = 1;
            masterVoiceVolume = 1;
        }

        private void Start()
        {
            CreateLoopingSounds();
        }

        private void CreateLoopingSounds()
        {
            foreach (SoundScriptableObject thisSound in soundsToLoop)
            {
                GameObject newObject = new();
                
                PlaySound playSound = newObject.AddComponent<PlaySound>();
                playSound.soundToPlay = thisSound;
                playSound.PlaySoundAtTransform(mainCameraTransform);

                RenameGameObjectFromSound(newObject, thisSound);

                DontDestroyOnLoad(newObject);
            }
        }


        #endregion

        #region Play Sounds

        public AudioPlayer CreateNewAudioPlayer(bool useDefaultParent = true)
        {
            GameObject newGameObject = new();
            RenameGameObjectFromSound(newGameObject);

            AudioPlayer audioPlayer = newGameObject.AddComponent<AudioPlayer>();
            if (useDefaultParent && defaultParentTransform != null)
            {
                newGameObject.transform.SetParent(defaultParentTransform);
            }

            return audioPlayer;
        }

        public GameObject RenameGameObjectFromSound(GameObject objectToRename, SoundScriptableObject soundSource = null)
        {
            string newName = "Audio Player";

            if (soundSource == null)
            {
                goto RenameObject;
            }

            newName += " - " + soundSource.soundName;

        RenameObject:
            objectToRename.name = newName;
            return objectToRename;
        }

        #endregion

        #region Volume Adjustment

        public void ChangeMasterMusicVolume(float newVolume)
        {
            masterMusicVolume = newVolume;

            foreach (AudioPlayer player in musicPlayers)
            {
                player.AdjustVolume(masterMusicVolume);
            }

            Debug.Log("Changing Master Music Volume to " + masterMusicVolume.ToString());
        }

        public void ChangeMasterSFXVolume(float newVolume)
        {
            masterSFXVolume = newVolume;

            foreach (AudioPlayer player in sfxPlayers)
            {
                player.AdjustVolume(masterSFXVolume);
            }

            Debug.Log("Changing Master SFX Volume to " + masterSFXVolume.ToString());
        }

        public void ChangeMasterVoiceVolume(float newVolume)
        {
            masterVoiceVolume = newVolume;

            foreach (AudioPlayer player in voicePlayers)
            {
                player.AdjustVolume(masterVoiceVolume);
            }

            Debug.Log("Changing Master Music Volume to " + masterVoiceVolume.ToString());
        }

        #endregion
    }
}