using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class AnimateScale : MonoBehaviour
    {
        [SerializeField] Vector2 animatedScale;
        [ValidateInput("ValidateGreaterThan0", "Seconds to complete should be more than 0", InfoMessageType.Warning)]
        [SerializeField] float secsToComplete;
        [SerializeField] LeanTweenType easeType;
        [SerializeField] bool loopTween = true;

        private Vector2 startScale;
        private Vector2 lastScale;

        private void Start()
        {
            if (animatedScale == Vector2.zero)
            {
                return;
            }

            startScale = transform.localScale;
            lastScale = startScale;
            StartNewTween();
        }

        private void StartNewTween()
        {
            Vector2 targetScale = GetNewScale();
            LTDescr tween = LeanTween.scale(gameObject, targetScale, secsToComplete);
            tween.setEase(easeType);
            if (loopTween)
            {
                tween.setOnComplete(StartNewTween);
            }

            lastScale = targetScale;

        }

        private Vector2 GetNewScale()
        {
            if (lastScale != startScale)
            {
                return startScale;
            }

            return animatedScale;

        }

        private bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }
    }
}
