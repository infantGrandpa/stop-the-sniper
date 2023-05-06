using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class AnimationBehaviour : MonoBehaviour
    {
        [ValidateInput("ValidateGreaterThan0", "Seconds to complete may change if controlled by another object.", InfoMessageType.Info)]
        public float secsToComplete;
        [SerializeField] protected Ease easeType;
        [SerializeField] protected bool pingPongTween;
        [SerializeField] protected bool tweenAtStart;

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

        }

        protected void OnDestroy()
        {
            currentTween.Kill(false);
        }

        protected bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }

        protected virtual void PingPongTween()
        {
            StartNewTween();
        }

        public float GetAnimationSecsElapsed()
        {
            if (!currentTween.IsActive())
            {
                return 0f;
            }

            return currentTween.Elapsed();
        }
    }
}
