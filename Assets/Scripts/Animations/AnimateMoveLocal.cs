using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace SniperProject
{
    public class AnimateMoveLocal : MonoBehaviour
    {
        [SerializeField] Vector2 moveRange;
        [ValidateInput("ValidateGreaterThan0", "Seconds to complete should be more than 0", InfoMessageType.Warning)]
        [SerializeField] float secsToComplete;
        [SerializeField] Ease easeType;
        [SerializeField] bool loopTween = true;
        
        private Vector2 startPosition;
        private Vector2 lastPosition;

        private Tweener currentTween;

        private void Start()
        {
            if (moveRange == Vector2.zero)
            {
                return;
            }

            startPosition = transform.localPosition;
            lastPosition = startPosition;
            StartNewTween();
        }

        private void StartNewTween()
        {
            Vector2 targetPosition = GetMovePosition();
            currentTween = transform.DOLocalMove(targetPosition, secsToComplete);
            currentTween.SetEase(easeType);

            if (loopTween)
            {
                currentTween.OnComplete(StartNewTween);
            }

            lastPosition = targetPosition;

        }

        private Vector2 GetMovePosition()
        {
            if (lastPosition != startPosition)
            {
                return startPosition;
            }

            return moveRange;

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
