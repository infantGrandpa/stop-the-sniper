using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace SniperProject
{
    public class AnimateTextToPosition : AnimationBehaviour
    {
        enum MoveLocation
        {
            Center,
            UpperLeft,
            UpperRight,
            LowerRight,
            LowerLeft,
            Custom
        }

        private TMP_Text textToMove;
        [SerializeField] MoveLocation moveLocation;

        [ShowIf("IsCustomMoveLocation")]
        [SerializeField] Vector2 customLocation;

        [HideIf("IsCustomMoveLocation")]
        [SerializeField] Vector2 locationOffset;


        private Vector2 startPosition;
        private Vector2 lastPosition;

        private Vector2 targetPosition;

        private void Awake()
        {
            textToMove = GetComponent<TMP_Text>();
            startPosition = transform.position;
            lastPosition = startPosition;
        }

        public override void StartNewTween(float newSecsToComplete = 0f)
        {
            targetPosition = GetMovePosition();
            base.StartNewTween(newSecsToComplete);

        }

        public override void ResetTween(float secsToReset)
        {
            targetPosition = startPosition;
            base.ResetTween(secsToReset);
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = transform.DOMove(targetPosition, secsToCompleteTween);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        protected override void PingPongTween()
        {
            targetPosition = lastPosition;
            lastPosition = transform.position;
            base.PingPongTween();
        }

        private Vector2 GetMovePosition()
        {
            if (lastPosition != startPosition)
            {
                return startPosition;
            }

            Vector2 presetPosition;

            switch (moveLocation)
            {

                case MoveLocation.Custom:
                    return customLocation;
                case MoveLocation.Center:
                    presetPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                    break;
                case MoveLocation.UpperLeft:
                    presetPosition = new Vector2(0, Screen.height);
                    break;
                case MoveLocation.UpperRight:
                    presetPosition = new Vector2(Screen.width, Screen.height);
                    break;
                case MoveLocation.LowerRight:
                    presetPosition = new Vector2(Screen.width, 0);
                    break;
                case MoveLocation.LowerLeft:
                    presetPosition = Vector2.zero;
                    break;
                default:
                    presetPosition = Vector2.zero;
                    break;
            }

            return presetPosition + locationOffset;

        }

        private bool IsCustomMoveLocation()
        {
            return moveLocation == MoveLocation.Custom;
        }
    }
}
