using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WeaponAnimationController : MonoBehaviour
    {
        [SerializeField] List<AnimationBehaviour> animations = new();

        [SerializeField] float secsToResetAnimations;

        private float secsRemainingUntilReset;

        public void StartAnimationsAfterReset(float secsToComplete)
        {
            StartCoroutine(WaitForResetCoroutine(secsToComplete));
        }

        private void StartTweens(float secsToComplete)
        {
            foreach (AnimationBehaviour thisAnimation in animations)
            {
                thisAnimation.StartNewTween(secsToComplete);
            }
        }

        private void Update()
        {
            secsRemainingUntilReset -= Time.deltaTime;
        }

        public void ResetAnimations()
        {
            secsRemainingUntilReset = secsToResetAnimations;

            foreach (AnimationBehaviour thisAnimation in animations)
            {
                thisAnimation.ResetTween(secsToResetAnimations);
            }
        }

        private IEnumerator WaitForResetCoroutine(float secsToComplete)
        {
            float secsToDelayNewAnimations =  Mathf.Max(secsRemainingUntilReset, 0f);
            yield return new WaitForSeconds(secsToDelayNewAnimations);

            float shortenedSecsToComplete = Mathf.Max(secsToComplete - secsToDelayNewAnimations, 0f);
            StartTweens(shortenedSecsToComplete);
        }
    }
}
