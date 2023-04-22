using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class SetWaypointOnSpawn : MonoBehaviour, IOnSpawn
    {
        public Waypoint waypoint;

        public void OnSpawn(GameObject spawnedObject)
        {
            if (waypoint == null)
            {
                Debug.LogError("ERROR SetWaypointOnSpawn: Waypoint on " + gameObject.name + " not set.", this);
                return;
            }
            
            if (!spawnedObject.TryGetComponent(out MoveTowardsWaypoint moveOrder)) {
                return;
            }

            moveOrder.SetTargetWaypoint(waypoint);
        }
    }
}
