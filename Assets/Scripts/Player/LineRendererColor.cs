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


        public void TweenLineColor(float secsToComplete)
        {
            LeanTween.color(lineRenderer.gameObject, armedColor, secsToComplete);
            
        }

        public void ResetLineColor()
        {
            lineRenderer.startColor = unarmedColor;
            lineRenderer.endColor = unarmedColor;
        }
    }
}
