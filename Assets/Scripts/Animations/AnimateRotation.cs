using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateRotation : AnimationBehaviour
    {
        [SerializeField] Vector3 animatedRotation;

        private Vector3 startRotation;
        private Vector3 lastRotation;

        private Vector3 targetRotation;

        private void Awake()
        {
            startRotation = transform.eulerAngles;
            lastRotation = startRotation;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            targetRotation = GetNewRotation();
            base.StartNewTween(newSecsToComplete);
            lastRotation = targetRotation;
        }

        public override void ResetTween(float secsToReset)
        {
            targetRotation = startRotation;
            base.ResetTween(secsToReset);
            lastRotation = startRotation;
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = transform.DORotate(targetRotation, secsToCompleteTween);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        private Vector3 GetNewRotation()
        {
            if (lastRotation != startRotation)
            {
                return startRotation;
            }

            return animatedRotation;

        }
    }
}
