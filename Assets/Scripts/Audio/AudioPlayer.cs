using System.Collections.Generic;
using UnityEngine;

namespace AudioManager
{
    public class AudioPlayer : MonoBehaviour
    {
        #region Properties and Variables

        private AudioSource audioSource;
        private SoundType soundType;
        private SoundScriptableObject originalSound;

        /// <summary>A reference to a list stored in the audio manager</summary>
        private List<AudioPlayer> audioPlayerList = null;

        #endregion

        #region Spawning and Destroying

        private void OnDisable()
        {
            if (audioPlayerList == null)
            {
                audioPlayerList = GetAudioPlayerList();
            }
            audioPlayerList.Remove(this);
        }

        /// <summary>
        /// Gets or adds all the components that are required for this behaviour.
        /// </summary>
        private void GetAllComponents()
        {
            if (audioSource != null)
            {
                return;
            }

            if (!TryGetComponent(out audioSource))
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public AudioSource InitializeAudioSource(SoundType newSoundType)
        {
            GetAllComponents();
            soundType = newSoundType;
            AddToAudioPlayerList();

            return audioSource;
        }


        /// <summary>
        /// Adds our audio player to the audio manager's list
        /// </summary>
        private void AddToAudioPlayerList()
        {
            if (audioPlayerList != null)
            {
                audioPlayerList.Remove(this);
                audioPlayerList = null;
            }

            audioPlayerList = GetAudioPlayerList();


            audioPlayerList.Add(this);
        }



        /// <summary>
        /// Gets the proper audio player list from the audio manager based on the this player's sound type.
        /// </summary>
        /// <returns>The list from the audio manager that matches this player's sound type.</returns>
        private List<AudioPlayer> GetAudioPlayerList()
        {
            List<AudioPlayer> newAudioPlayerList = null;
            switch (soundType)
            {
                case SoundType.music:
                    newAudioPlayerList = AudioManager.Instance.musicPlayers;
                    break;
                case SoundType.voice:
                    newAudioPlayerList = AudioManager.Instance.voicePlayers;
                    break;
                case SoundType.sfx:
                    newAudioPlayerList = AudioManager.Instance.sfxPlayers;
                    break;
            }

            if (newAudioPlayerList == null)
            {
                Debug.LogError("ERROR AudioPlayer OnEnable: " + gameObject.name + " does not have a valid sound type.", this);
                return null;
            }

            return newAudioPlayerList;
        }

        #endregion

        #region On Update

        private void Update()
        {
            if (audioSource == null)
            {
                return;
            }

            if (audioSource.loop || audioSource.isPlaying)
            {
                return;
            }

            Destroy(gameObject);
        }

        public void AdjustVolume(float masterVolume)
        {
            audioSource.volume = originalSound.volume * masterVolume;
        }

        #endregion
    }
}