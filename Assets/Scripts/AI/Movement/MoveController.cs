using UnityEngine;
using Pathfinding;

namespace SniperProject
{
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AIPath))]
    public class MoveController : MonoBehaviour
    {
        [SerializeField, Range(0, 360f)] 
        float maxTurningRadius = 360f;

        private Transform targetTransform;
        private AIDestinationSetter destinationSetter;
        private AIPath path;

        private IMoveOrder moveOrder;

        private Vector2 targetDestinationPosition;
        private Vector2 tempDestinationPosition;

        private void Awake()
        {
            destinationSetter = GetComponent<AIDestinationSetter>();
            path = GetComponent<AIPath>();
            moveOrder = GetComponent<IMoveOrder>();
            CreateMoveTarget();
        }

        private void Update()
        {
            CheckArrivalStatus();
        }

        private void CreateMoveTarget()
        {
            targetTransform = new GameObject().transform;
            targetTransform.name = gameObject.name + "_destination";
            
            targetTransform.SetParent(LevelManager.Instance.DynamicTransform);
            SetMovePosition(transform.position);
            
            destinationSetter.target = targetTransform;
        }

        private void CheckArrivalStatus()
        {
            if (!path.reachedDestination)
            {
                return;
            }

            if (moveOrder == null)
            {
                return;
            }

            //Add Turning stuff

            moveOrder.ArrivedAtDestination();
        }

        private void OnDestroy()
        {
            if (targetTransform == null)
            {
                return;
            }

            Destroy(targetTransform.gameObject);
            targetTransform = null;
        }

        public void SetMovePosition(Vector3 newMovePosition)
        {
            //CreateTurningDestination(newMovePosition);
            targetTransform.position = newMovePosition;

            Debug.DrawLine(transform.position, targetTransform.position, Color.magenta, 1f);
        }


        private void CreateTurningDestination(Vector2 newMovePosition)
        {
            targetDestinationPosition = newMovePosition;
            Vector3 direction = targetDestinationPosition - (Vector2)transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Check if the angle is greater than the maximum turning radius
            float currentAngle = transform.eulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
            bool needsTurn = Mathf.Abs(angleDifference) > maxTurningRadius;


            if (needsTurn)
            {
                Vector2 tempPosition = transform.position + Quaternion.Euler(0f, 0f, maxTurningRadius) * direction.normalized;
                targetTransform.position = tempPosition;
                return;
            }

            targetTransform.position = targetDestinationPosition;
            
        }

    }
}

