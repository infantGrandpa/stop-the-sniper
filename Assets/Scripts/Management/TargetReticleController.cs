using UnityEngine;
using UnityEngine.UI;

namespace SniperProject
{
    [RequireComponent(typeof (Image))]
    public class TargetReticleController : MonoBehaviour
    {
        private Image reticleImage;
        private AnimateMoveToPosition animateMove;
        private AnimateScale animateScale;

        private void Awake()
        {
            reticleImage = GetComponent<Image>();
            animateMove = GetComponent<AnimateMoveToPosition>();
            animateScale = GetComponent<AnimateScale>();
        }

        private void LateUpdate()
        {
            UpdateReticle();
        }

        private void UpdateReticle()
        {
            TargetBehaviour target = PlayerBehaviour.Instance.PlayerHomingSignal.CurrentTarget;

            if (target == null)
            {
                HideReticle();
                return;
            }

            ShowReticle();
            SetReticlePosition(target.transform.position);
        }

        public void HideReticle()
        {
            //Reset Size
            animateScale.ResetTween(0f);
            reticleImage.enabled = false;
        }

        public void ShowReticle()
        {
            reticleImage.enabled = true;
        }

        public void SetReticlePosition(Vector3 newPosition)
        {
            Vector3 screenPosition = MainCanvasBehaviour.Instance.ConvertWorldToCanvas(newPosition);
            if (animateMove == null)
            {
                transform.position = screenPosition;
            } 
            else
            {
                animateMove.TweenToNewPosition(screenPosition);
            }

            
        }
    }
}
