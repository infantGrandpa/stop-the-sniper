using DG.Tweening;
using UnityEngine;
using TMPro;

namespace SniperProject
{
    public class AnimateText : AnimationBehaviour
    {
        [SerializeField] string placeholderString = "";
        private TMP_Text text;
        private string targetString;


        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void StartTextAnimation(string newString)
        {
            targetString = newString;
            StartNewTween();
        }

        public override void StartNewTween(float newSecsToComplete = 0)
        {
            if (newSecsToComplete > 0f)
            {
                secsToComplete = newSecsToComplete;
            }

            ChangeTextToPlaceholder();
            //Tween(secsToComplete);
        }

        protected override void Tween(float secsToCompleteTween)
        {
            currentTween = text.DOText(targetString, secsToCompleteTween, true, ScrambleMode.None);
            currentTween.SetEase(easeType);

            base.Tween(secsToCompleteTween);
        }

        private void ChangeTextToPlaceholder()
        {
            currentTween = text.DOText(placeholderString, secsToComplete, true, ScrambleMode.All);
            currentTween.SetEase(easeType);
            //currentTween.OnComplete(ChangePlaceholderToTarget);
        }

        private void ChangePlaceholderToTarget()
        {
            Tween(secsToComplete);
        }
    }
}
