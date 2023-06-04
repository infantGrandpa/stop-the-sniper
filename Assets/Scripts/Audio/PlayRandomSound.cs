using System.Collections.Generic;
using UnityEngine;

namespace AudioManager
{
    public class PlayRandomSound : PlaySound
    {
        private List<SoundScriptableObject> soundPool = new();

        private void SelectNewRandomSound()
        {
            int randomIndex = Random.Range(0, soundPool.Count);
            soundToPlay = soundPool[randomIndex];
        }

        public override void PlaySoundHere()
        {
            SelectNewRandomSound();
            base.PlaySoundHere();
        }

        public override void PlaySoundAtPosition(Vector3 position, bool useDefaultParent = true)
        {
            SelectNewRandomSound();
            base.PlaySoundAtPosition(position, useDefaultParent);
        }

        public override void PlaySoundAtTransform(Transform transformParent)
        {
            SelectNewRandomSound();
            base.PlaySoundAtTransform(transformParent);
        }
    }
}
