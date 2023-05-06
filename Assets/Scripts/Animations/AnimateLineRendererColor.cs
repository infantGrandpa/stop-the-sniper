using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SniperProject
{
    
    public class AnimateLineRendererColor : AnimationBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Color unarmedColor;
        [SerializeField] Color armedColor;
        [PropertyRange(0, 1)]
        [SerializeField] float lineEndOpacity;

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
            Color2 currentColor2 = ConvertColorToGradient(lastColor);
            Color2 targetColor2 = ConvertColorToGradient(targetColor);

            currentTween = lineRenderer.DOColor(currentColor2, targetColor2, secsToCompleteTween);
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

        private Color2 ConvertColorToGradient(Color colorToConvert)
        {
            Color gradientStart = colorToConvert;
            Color gradientEnd = new(colorToConvert.r, colorToConvert.b, colorToConvert.b, lineEndOpacity);

            return new Color2(gradientStart, gradientEnd);
        }
    }
}
