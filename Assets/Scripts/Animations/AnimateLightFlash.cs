using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateLightFlash : MonoBehaviour
    {
        [SerializeField] Light lightToAnimate;
        [SerializeField] bool playOnStart;


        [Header("To Max Intensity")]
        [SerializeField] float maxIntensity;
        [SerializeField] float secsToMaxIntensity;
        [SerializeField] Ease maxEaseType;

        [Header("To Min Intensity")]
        [SerializeField] float minIntensity;
        [SerializeField] float secsToMinIntensity;
        [SerializeField] Ease minEaseType;

        private Sequence currentSequence;

        private void Start()
        {
            if (!playOnStart)
            {
                return;
            }

            Tween();
        }

        public void Tween()
        {
            currentSequence = DOTween.Sequence();
            //Flash to max intensity
            currentSequence.Append(lightToAnimate.DOIntensity(maxIntensity, secsToMaxIntensity).SetEase(maxEaseType));
            //Return to min intensity
            currentSequence.Append(lightToAnimate.DOIntensity(0, secsToMinIntensity).SetEase(minEaseType));

        }

        private void OnDestroy()
        {
            if (currentSequence == null)
            {
                return;
            }

            currentSequence.Kill();
        }
    }
}
