using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateMoveToPosition : AnimationBehaviour
    {
        private Vector2 lastPosition;
        private Vector2 targetPosition;

        private void Awake()
        {
            lastPosition = transform.position;
            targetPosition = lastPosition;
        }

        public void TweenToNewPosition(Vector2 newPosition)
        {
            currentTween.Kill();
            targetPosition = newPosition;
            StartNewTween();
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = transform.DOMove(targetPosition, secsToComplete);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        protected override void PingPongTween()
        {
            targetPosition = lastPosition;
            lastPosition = transform.position;
            base.PingPongTween();
        }
    }
}
