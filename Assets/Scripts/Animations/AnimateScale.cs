using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateScale : MonoBehaviour
    {
        [SerializeField] Vector2 animatedScale;
        [ValidateInput("ValidateGreaterThan0", "Seconds to complete should be more than 0", InfoMessageType.Warning)]
        [SerializeField] float secsToComplete;
        [SerializeField] Ease easeType;
        [SerializeField] bool loopTween = true;

        private Vector2 startScale;
        private Vector2 lastScale;

        private Tweener currentTween;

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
            currentTween = transform.DOScale(targetScale, secsToComplete);
            currentTween.SetEase(easeType);

            if (loopTween)
            {
                currentTween.OnComplete(StartNewTween);
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

        private void OnDestroy()
        {
            currentTween.Kill(false);
        }

        private bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }
    }
}
