using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateScale : AnimationBehaviour
    {
        [SerializeField] Vector2 animatedScale;

        private Vector2 startScale;
        private Vector2 lastScale;

        private Vector2 targetScale;


        private void Awake()
        {
            startScale = transform.localScale;
            lastScale = startScale;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            targetScale = GetNewScale();
            base.StartNewTween(newSecsToComplete);
            lastScale = targetScale;
        }

        public override void ResetTween(float secsToReset)
        {
            targetScale = startScale;
            base.ResetTween(secsToReset);
            lastScale = startScale;
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = transform.DOScale(targetScale, secsToCompleteTween);
            currentTween.SetEase(easeType);

            if (pingPongTween)
            {
                currentTween.OnComplete(PingPongTween);
            }
        }

        private Vector2 GetNewScale()
        {
            if (lastScale != startScale)
            {
                return startScale;
            }

            return animatedScale;

        }
    }
}
