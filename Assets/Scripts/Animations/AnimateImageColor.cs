using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SniperProject
{
    public class AnimateImageColor : AnimationBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Color unarmedColor;
        [SerializeField] Color armedColor;

        private Color startColor;
        private Color lastColor;

        private Color targetColor;


        private void Awake()
        {
            startColor = unarmedColor;
            lastColor = startColor;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            targetColor = GetNewColor();
            base.StartNewTween(newSecsToComplete);
            lastColor = targetColor;

        }

        public override void ResetTween(float secsToReset)
        {
            targetColor = startColor;
            base.ResetTween(secsToReset);
            lastColor = startColor;
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = image.DOColor(targetColor, secsToCompleteTween);
            currentTween.SetEase(easeType);

            if (pingPongTween)
            {
                currentTween.OnComplete(PingPongTween);
            }
        }

        private Color GetNewColor()
        {
            if (lastColor != armedColor)
            {
                return armedColor;
            }

            return unarmedColor;
        }

    }
}
