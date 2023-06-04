using UnityEngine;

namespace AudioManager
{
    public class PlaySound : MonoBehaviour
    {
        public SoundScriptableObject soundToPlay;

        private AudioSource audioSource;
        private bool muteThisSound;

        #region Play Sounds

        public virtual void PlaySoundHere()
        {
            PlaySoundAtPosition(transform.position);
        }

        public virtual void PlaySoundAtPosition(Vector3 position, bool useDefaultParent = true)
        {
            if (soundToPlay == null)
            {
                Debug.LogError("ERROR AudioManager PlaySound: Provided sound is null", this);
                return;
            }

            AudioPlayer audioPlayer = CreateAudioPlayerObject(useDefaultParent);
            audioPlayer.transform.position = position;

            CreateAudioSource(audioPlayer);
        }

        public virtual void PlaySoundAtTransform(Transform transformParent)
        {
            if (soundToPlay == null)
            {
                Debug.LogError("ERROR AudioManager PlaySound: Provided sound is null", this);
                return;
            }

            AudioPlayer audioPlayer = CreateAudioPlayerObject();
            audioPlayer.transform.SetParent(transformParent);

            CreateAudioSource(audioPlayer);
        }


        protected virtual void ConvertSoundToAudioClip(AudioSource audioSource)
        {
            audioSource.clip = soundToPlay.clip;
            audioSource.volume = soundToPlay.volume;
            audioSource.pitch = soundToPlay.pitch;
            audioSource.loop = soundToPlay.loop;
            audioSource.mute = soundToPlay.mute;

            if (muteThisSound)
            {
                audioSource.mute = true;
            }

            audioSource.Play();
        }

        private AudioPlayer CreateAudioPlayerObject(bool useDefaultParent = false)
        {
            AudioPlayer audioPlayer = AudioManager.Instance.CreateNewAudioPlayer(useDefaultParent);
            AudioManager.Instance.RenameGameObjectFromSound(audioPlayer.gameObject, soundToPlay);

            return audioPlayer;
        }

        private void CreateAudioSource(AudioPlayer audioPlayer)
        {
            if (audioSource != null)
            {
                Destroy(audioSource);
            }

            audioSource = audioPlayer.InitializeAudioSource(soundToPlay.soundType);
            ConvertSoundToAudioClip(audioSource);

        }

        public void Mute()
        {
            if (audioSource != null)
            {
                audioSource.mute = true;
                return;
            }

            muteThisSound = true;
        }

        public void Unmute()
        {
            if (audioSource != null)
            {
                audioSource.mute = false;
                return;
            }

            muteThisSound = false;
        }

        #endregion
    }
}
