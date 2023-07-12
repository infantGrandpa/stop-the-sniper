using UnityEngine;
using Sirenix.OdinInspector;
using Pathfinding;

namespace SniperProject
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public TargetBehaviour CurrentTarget { get; private set; }

        [SerializeField, ValidateInput("ValidateGreaterThan0", "Attack Range must be greater than 0.", InfoMessageType.Error)]
        private float attackRange;

        private MoveController moveController;
        private AIPath path;

        private float normalMaxSpeed;

        private void Awake()
        {
            moveController = GetComponent<MoveController>();
            path = GetComponent<AIPath>();
        }

        public void SetTarget(TargetBehaviour newTarget)
        {
            normalMaxSpeed = path == null ? 0 : path.maxSpeed;

            CurrentTarget = newTarget;
            if (CurrentTarget == null)
            {
                return;
            }
            CurrentTarget.OnInvalid.AddListener(GetNewTarget);

        }

        private bool ValidateGreaterThan0(float value)
        {
            return value > 0f;
        }

        public void GetNewTarget()
        {
            CurrentTarget?.OnInvalid.RemoveListener(GetNewTarget);

            TargetBehaviour newTarget = GetClosestTarget();

            CurrentTarget = newTarget;
        }

        private TargetBehaviour GetClosestTarget()
        {
            TargetBehaviour closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach(TargetBehaviour thisTarget in LevelManager.Instance.targets)
            {
                if (!thisTarget.IsValidTarget)
                {
                    continue;
                }

                float distanceToTarget = Vector2.Distance(transform.position, thisTarget.transform.position);
                if (distanceToTarget > closestDistance)
                {
                    continue;
                }

                closestDistance = distanceToTarget;
                closestTarget = thisTarget;
            }

            return closestTarget;
        }

        public bool IsCurrentTargetInRange()
        {
            float distanceToTarget = Vector2.Distance(transform.position, CurrentTarget.transform.position);
            if (distanceToTarget <= attackRange)
            {
                return true;
            }

            return false;
        }

        public void SetDestination(Vector2 newDestination)
        {
            if (moveController == null)
            {
                return;
            }

            moveController.SetMovePosition(newDestination);
        }

        public void SetDestinationToCurrentTarget()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            SetDestination(CurrentTarget.transform.position);
        }

        public void SetSpeed(float newSpeed)
        {
            if (path == null)
            {
                return;
            }

            path.maxSpeed = newSpeed;
        }

        public void ResetSpeed()
        {
            SetSpeed(normalMaxSpeed);
        }

        public void MatchTargetSpeed()
        {
            float newSpeed = normalMaxSpeed / 2; //Slow down if the target doesn't have a maxSpeed
            if (!CurrentTarget.TryGetComponent(out AIPath targetPath))
            {
                newSpeed = Mathf.Min(targetPath.maxSpeed, normalMaxSpeed);      //Can't speed up faster than their max speed
            }

            SetSpeed(newSpeed);
        }
    }
}
