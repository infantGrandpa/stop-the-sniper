using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateLightFlash : MonoBehaviour
    {
        [SerializeField] Light lightToAnimate;

        [Header("To Max Intensity")]
        [SerializeField] float maxIntensity;
        [SerializeField] float secsToMaxIntensity;
        [SerializeField] Ease maxEaseType;

        [Header("To Min Intensity")]
        [SerializeField] float minIntensity;
        [SerializeField] float secsToMinIntensity;
        [SerializeField] Ease minEaseType;



        public void Tween()
        {
            Sequence mySequence = DOTween.Sequence();
            //Flash to max intensity
            mySequence.Append(lightToAnimate.DOIntensity(maxIntensity, secsToMaxIntensity).SetEase(maxEaseType));
            mySequence.Append(lightToAnimate.DOIntensity(0, secsToMinIntensity).SetEase(minEaseType));

        }
    }
}
