using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;

namespace SniperProject
{
    public class AnimationBehaviour : MonoBehaviour
    {
        [ValidateInput("ValidateGreaterThan0", "Seconds to complete may change if controlled by another object.", InfoMessageType.Info)]
        public float secsToComplete;
        [SerializeField] protected Ease easeType;
        [SerializeField] protected bool pingPongTween;
        [SerializeField] protected bool tweenAtStart;

        [Header("On Animation Complete")]
        [SerializeField] UnityEvent onCompleteEvent;
        [SerializeField] float secsToDelayNextEvent;

        protected Tweener currentTween;

        protected virtual void Start()
        {
            if (tweenAtStart)
            {
                StartNewTween();
            }
        }



        public virtual void StartNewTween(float newSecsToComplete = 0f)
        {
            if (newSecsToComplete > 0f)
            {
                secsToComplete = newSecsToComplete;
            }

            Tween(secsToComplete);
        }

        public virtual void ResetTween(float secsToReset)
        {
            Tween(secsToReset);
        }

        protected virtual void Tween(float secsToCompleteTween)
        {
            if (currentTween == null)
            {
                return;
            }

            currentTween.OnComplete(OnAnimationComplete);
        }

        protected virtual void PingPongTween()
        {
            StartNewTween();
        }

        protected virtual void OnAnimationComplete()
        {
            StartCoroutine(OnAnimationCompleteCoroutine());
        }

        protected virtual IEnumerator OnAnimationCompleteCoroutine()
        {
            yield return new WaitForSeconds(secsToDelayNextEvent);

            onCompleteEvent?.Invoke();

            if (pingPongTween)
            {
                PingPongTween();
            }
        }

        protected void OnDestroy()
        {
            currentTween.Kill(false);
        }

        public float GetAnimationSecsElapsed()
        {
            if (!currentTween.IsActive())
            {
                return 0f;
            }

            return currentTween.Elapsed();
        }
        protected bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }
    }
}
