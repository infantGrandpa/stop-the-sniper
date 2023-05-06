using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class HomingSignal : MonoBehaviour
    {
        [SerializeField] LineRenderer myLine;

        [SerializeField] float circleCastRadius;

        public TargetBehaviour CurrentTarget { get; private set; }

        private void Update()
        {
            CalculateClosestTarget();
        }

        private void SetCurrentTarget(TargetBehaviour newTarget)
        {
            CurrentTarget = newTarget;
        }

        private void CalculateClosestTarget()
        {
            if (FindTargetCircleCast())
            {
                return;
            }

            FindTargetFromLine();
            
        }

        private bool FindTargetCircleCast()
        {
            Vector3 linePointA = transform.TransformPoint(myLine.GetPosition(0));
            Vector3 linePointB = transform.TransformPoint(myLine.GetPosition(1));
            Vector3 direction = (linePointB - linePointA).normalized;

            RaycastHit2D hit = Physics2D.CircleCast(linePointA, circleCastRadius, direction, References.maxDistanceInLevel, References.targetLayerMaskInt);
            if (!hit)
            {
                return false;
            }

            if (!hit.transform.TryGetComponent(out TargetBehaviour hitTarget))
            {
                Debug.LogError("ERROR HomingSignal FindTargetCircleCast: Target (" + hit.transform.name + ") does not have a CharacterBehaviour Component but is on the Target Layer.");
                return false;
            }

            if (!hitTarget.IsTargetValid())
            {
                return false;
            }

            SetCurrentTarget(hitTarget);
            return true;
        }

        private bool FindTargetFromLine()
        {
            float closestDistanceToTarget = Mathf.Infinity;
            TargetBehaviour closestTarget = null;
            foreach (TargetBehaviour thisTarget in LevelManager.Instance.targets)
            {
                float distanceToThisTarget = CalculateDistanceFromTransformToLine(thisTarget.transform);
                if (distanceToThisTarget > closestDistanceToTarget)
                {
                    continue;
                }

                closestDistanceToTarget = distanceToThisTarget;
                closestTarget = thisTarget;
            }

            bool targetFound = closestTarget != null;
            SetCurrentTarget(closestTarget);

            return targetFound;
        }

        private float CalculateDistanceFromTransformToLine(Transform targetTransform)
        {
            Vector3 linePointA = transform.TransformPoint(myLine.GetPosition(0));
            Vector3 linePointB = transform.TransformPoint(myLine.GetPosition(1));
            Vector3 targetPosition = targetTransform.position;

            Vector3 direction = (linePointA - linePointB).normalized;
            Vector3 projectedVector = Vector3.Project(targetPosition - linePointA, direction);

            Vector3 closestPoint = linePointA + projectedVector;
            float distance = Vector3.Distance(targetPosition, closestPoint);

            Debug.DrawLine(targetPosition, closestPoint, Color.red);
            return distance;
        }

        private IEnumerator RecalcuateDistance()
        {
            yield return new WaitForSeconds(2f);
            //CalculateDistanceFromTransformToLine();

            StartCoroutine(RecalcuateDistance());
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 linePointA = transform.TransformPoint(myLine.GetPosition(0));
            Vector3 linePointB = transform.TransformPoint(myLine.GetPosition(1));
            Gizmos.DrawWireSphere(linePointA, 0.25f);
            Gizmos.DrawWireSphere(linePointB, 0.25f);

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(worldPosition, circleCastRadius);

            if (CurrentTarget == null)
            {
                return;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CurrentTarget.transform.position, 0.5f);
        }
    }
}