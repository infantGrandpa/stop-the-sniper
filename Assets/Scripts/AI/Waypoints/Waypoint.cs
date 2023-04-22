using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] Waypoint nextWaypoint;
        private IWaypointOnArrive onArrive;

        private void Awake()
        {
            onArrive = GetComponent<IWaypointOnArrive>();
        }

        private void OnEnable()
        {
            SpawnController.Instance.waypoints.Add(this);
        }

        private void OnDisable()
        {
            if (SpawnController.Instance == null)
            {
                return;
            }
            SpawnController.Instance.waypoints.Remove(this);
        }

        public Waypoint GetNextWaypoint()
        {
            return nextWaypoint;
        }

        public void ArrivedAtDestination(GameObject arrivedObject)
        {
            if (onArrive == null)
            {
                return;
            }

            onArrive.OnArrival(arrivedObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.15f);
        }
    }
}
