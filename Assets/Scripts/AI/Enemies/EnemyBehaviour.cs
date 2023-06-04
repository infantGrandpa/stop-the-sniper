using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public TargetBehaviour CurrentTarget { get; private set; }

        [ValidateInput("ValidateGreaterThan0", "Attack Range must be greater than 0.", InfoMessageType.Error)]
        public float attackRange;

        public void SetTarget(TargetBehaviour newTarget)
        {
            CurrentTarget = newTarget;
        }

        private bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }
    }
}
