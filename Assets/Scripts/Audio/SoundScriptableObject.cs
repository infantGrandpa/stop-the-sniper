using UnityEngine;
namespace AudioManager
{
    [CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/Sound")]
    public class SoundScriptableObject : ScriptableObject
    {
        public string soundName;
        public AudioClip clip;
        public SoundType soundType;

        [Range(0, 1)]
        public float volume = 1f;

        [Range(0, 1)]
        public float pitch = 1f;

        public bool loop;
        public bool mute;
    }
}