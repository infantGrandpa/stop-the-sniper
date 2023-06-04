using UnityEngine;

namespace AudioManager
{
    public class PlaySound : MonoBehaviour
    {
        public SoundScriptableObject soundToPlay;

        #region Play Sounds

        public virtual void PlaySoundHere()
        {
            PlaySoundAtPosition(transform.position);
        }

        public virtual void PlaySoundAtPosition(Vector3 position, bool useDefaultParent = true)
        {
            if (soundToPlay == null)
            {
                Debug.LogError("ERROR AudioManager PlaySound: Provided sound is null");
                return;
            }

            AudioPlayer audioPlayer = AudioManager.Instance.CreateNewAudioPlayer(useDefaultParent);
            AudioManager.Instance.RenameGameObjectFromSound(audioPlayer.gameObject, soundToPlay);
            audioPlayer.transform.position = position;
            audioPlayer.InitSound(soundToPlay);
        }

        public virtual void PlaySoundAtTransform(Transform transformParent)
        {
            if (soundToPlay == null)
            {
                Debug.LogError("ERROR AudioManager PlaySound: Provided sound is null");
                return;
            }

            AudioPlayer audioPlayer = AudioManager.Instance.CreateNewAudioPlayer(false);
            AudioManager.Instance.RenameGameObjectFromSound(audioPlayer.gameObject, soundToPlay);
            audioPlayer.transform.SetParent(transformParent);
            audioPlayer.InitSound(soundToPlay);
        }

        #endregion
    }
}
