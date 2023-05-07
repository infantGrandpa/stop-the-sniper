using TMPro;
using DG.Tweening;
using UnityEngine;

namespace SniperProject
{
    public class AnimateTextScale : AnimationBehaviour
    {
        [SerializeField] float animatedScale;

        private TMP_Text textToScale;
        private float startScale;
        private float lastScale;

        private float targetScale;


        private void Awake()
        {
            textToScale = GetComponent<TextMeshProUGUI>();
            startScale = 1f;
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
            currentTween = textToScale.DOScale(targetScale, secsToCompleteTween);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        private float GetNewScale()
        {
            if (lastScale != startScale)
            {
                return startScale;
            }

            return animatedScale;

        }
    }
}
