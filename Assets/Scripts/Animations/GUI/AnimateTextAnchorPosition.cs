using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class AnimateTextAnchorPosition : AnimationBehaviour
    {
        enum AnchorPosition
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        [SerializeField] AnchorPosition anchorPosition;

        private RectTransform rectTransform;
        private Vector2 startMin;
        private Vector2 startMax;
        private Vector2 lastMin;
        private Vector2 lastMax;

        private Vector2 targetMin;
        private Vector2 targetMax;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            startMin = rectTransform.anchorMin;
            startMax = rectTransform.anchorMax;
            lastMin = startMin;
            lastMax = startMax;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            SetTargetPositions();
            base.StartNewTween(newSecsToComplete);

        }

        public override void ResetTween(float secsToReset)
        {
            targetMin = startMin;
            targetMax = startMax;
            base.ResetTween(secsToReset);
        }

        protected override void Tween(float secsToCompleteTween)
        {
            rectTransform.DOAnchorMin(targetMin, secsToCompleteTween).SetEase(easeType).OnComplete(OnAnimationComplete);
            rectTransform.DOAnchorMax(targetMax, secsToCompleteTween).SetEase(easeType).OnComplete(OnAnimationComplete);
        }

        private void SetTargetPositions()
        {
            if (lastMin != startMin || lastMax != startMax)
            {
                targetMin = startMin;
                targetMax = startMax;
            }


            switch (anchorPosition)
            {
                case AnchorPosition.TopLeft:
                    targetMin = new Vector2(0, 1);
                    targetMax = new Vector2(0, 1);
                    break;
                case AnchorPosition.TopCenter:
                    targetMin = new Vector2(0.5f, 1);
                    targetMax = new Vector2(0.5f, 1);
                    break;
                case AnchorPosition.TopRight:
                    targetMin = new Vector2(1, 1);
                    targetMax = new Vector2(1, 1);
                    break;
                case AnchorPosition.MiddleLeft:
                    targetMin = new Vector2(0, 0.5f);
                    targetMax = new Vector2(0, 0.5f);
                    break;
                case AnchorPosition.MiddleCenter:
                    targetMin = new Vector2(0.5f, 0.5f);
                    targetMax = new Vector2(0.5f, 0.5f);
                    break;
                case AnchorPosition.MiddleRight:
                    targetMin = new Vector2(1, 0.5f);
                    targetMax = new Vector2(1, 0.5f);
                    break;
                case AnchorPosition.BottomLeft:
                    targetMin = new Vector2(0, 0);
                    targetMax = new Vector2(0, 0);
                    break;
                case AnchorPosition.BottomCenter:
                    targetMin = new Vector2(0.5f, 0);
                    targetMax = new Vector2(0.5f, 0);
                    break;
                case AnchorPosition.BottomRight:
                    targetMin = new Vector2(1, 0);
                    targetMax = new Vector2(1, 0);
                    break;
            }

            

        }
    }
}
