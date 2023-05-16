using DG.Tweening;
using UnityEngine;
using TMPro;

namespace SniperProject
{
    public class AnimateCounter : AnimationBehaviour
    {
        private TMP_Text text;
        private int lastWave = -1;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        protected override void Tween(float secsToCompleteTween)
        {
            int nextWave = WaveManager.Instance.CurrentWaveIndex;
            currentTween = text.DOCounter(lastWave, nextWave, secsToCompleteTween);
            currentTween.SetEase(easeType);

            lastWave = nextWave;

            base.Tween(secsToCompleteTween);
        }
    }
}
