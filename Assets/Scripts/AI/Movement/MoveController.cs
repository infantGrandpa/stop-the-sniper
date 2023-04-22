using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace SniperProject
{
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AIPath))]
    public class MoveController : MonoBehaviour
    {
        private Transform targetTransform;
        private AIDestinationSetter destinationSetter;
        private AIPath path;

        private IMoveOrder moveOrder;

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
            targetTransform.parent = LevelManager.Instance.DynamicTransform;

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
            targetTransform.position = newMovePosition;
        }

    }
}

