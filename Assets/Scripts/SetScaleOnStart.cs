using UnityEngine;

namespace SniperProject
{
    public class SetScaleOnStart : MonoBehaviour
    {
        [SerializeField] Vector3 targetScale;

        private void Start()
        {
            bool success = SetRectTransformScale();
            if (success)
            {
                return;
            }

            SetTransformScale();
        }

        private void SetTransformScale()
        {
            transform.localScale = targetScale;
        }

        private bool SetRectTransformScale()
        {
            if (!TryGetComponent(out RectTransform myRectTransform))
            {
                return false;
            }

            myRectTransform.localScale = targetScale;

            return true;
        }
    }
}
