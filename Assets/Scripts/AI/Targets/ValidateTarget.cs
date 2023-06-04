using UnityEngine;

namespace SniperProject
{
    public class ValidateTarget : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out TargetBehaviour targetBehaviour))
            {
                return;
            }

            targetBehaviour.MarkTargetValid();
        }
    }
}
