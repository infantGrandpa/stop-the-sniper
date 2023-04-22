using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    
    public class LineRendererColor : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Color unarmedColor;
        [SerializeField] Color armedColor;
        [SerializeField] float secsToReset;

        private Tweener currentTween;

        public void TweenLineColor(float secsToComplete)
        {
            currentTween.Kill();
            Color2 startColor = new(unarmedColor, unarmedColor);
            Color2 endColor = new(armedColor, armedColor);
            currentTween = lineRenderer.DOColor(startColor, endColor, secsToComplete);
            currentTween.SetEase(Ease.InQuad);
        }

        public void ResetLineColor()
        {
            currentTween.Kill();
            Color2 startColor = new(armedColor, armedColor);
            Color2 endColor = new(unarmedColor, unarmedColor);
            lineRenderer.DOColor(startColor, endColor, secsToReset);
        }

        private void OnDestroy()
        {
            currentTween.Kill(false);
        }
    }
}
