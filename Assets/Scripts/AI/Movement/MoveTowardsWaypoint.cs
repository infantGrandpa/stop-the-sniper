using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    [RequireComponent(typeof(MoveController))]
    public class MoveTowardsWaypoint : MonoBehaviour, IMoveOrder
    {

        public Waypoint targetWaypoint;
        
        private MoveController moveController;

        private void Awake()
        {
            moveController = GetComponent<MoveController>();
        }

        private void Start()
        {
            if (targetWaypoint != null)
            {
                return;
            }
            DebugHelper.LogWarning("Target waypoint not set for " + gameObject.name + ". Choosing random waypoint...");
            SetTargetWaypoint(SpawnController.Instance.GetRandomWaypoint());
        }


        public void ArrivedAtDestination()
        {
            if (targetWaypoint == null)
            {
                return;
            }

            targetWaypoint.ArrivedAtDestination(gameObject);
        }

        public void SetTargetWaypoint(Waypoint newWaypointTransform)
        {
            targetWaypoint = newWaypointTransform;
            moveController.SetMovePosition(targetWaypoint.transform.position);
        }


    }
}
