using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateMoveLocal : AnimationBehaviour
    {
        [SerializeField] Vector2 tweenOffset;
        
        private Vector2 startPosition;
        private Vector2 lastPosition;

        private Vector2 targetPosition;


        private void Awake()
        {
            startPosition = transform.localPosition;
            lastPosition = startPosition;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            targetPosition = GetMovePosition();
            base.StartNewTween(newSecsToComplete);
            lastPosition = targetPosition;

        }

        public override void ResetTween(float secsToReset)
        {
            targetPosition = startPosition;
            base.ResetTween(secsToReset);
            lastPosition = startPosition;
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = transform.DOLocalMove(targetPosition, secsToComplete);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        private Vector2 GetMovePosition()
        {
            if (lastPosition != startPosition)
            {
                return startPosition;
            }

            return startPosition + tweenOffset;

        }

    }
}
