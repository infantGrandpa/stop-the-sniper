using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class InvalidateTarget : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out TargetBehaviour targetBehaviour))
            {
                return;
            }

            targetBehaviour.MarkTargetInvalid();
        }
    }
}
