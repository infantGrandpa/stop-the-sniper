using UnityEngine;
using UnityEngine.UI;

namespace SniperProject
{
    [RequireComponent(typeof (Image))]
    public class TargetReticleController : MonoBehaviour
    {
        public static TargetReticleController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(TargetReticleController)) as TargetReticleController;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static TargetReticleController _instance;

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
                //HideReticle();
                SetReticlePosition(Input.mousePosition, false);
                return;
            }

            //ShowReticle();
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

        public void SetReticlePosition(Vector3 newPosition, bool isWorldPosition = true)
        {
            Vector3 screenPosition = isWorldPosition ? MainCanvasBehaviour.Instance.ConvertWorldToCanvas(newPosition) : newPosition;
          
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
