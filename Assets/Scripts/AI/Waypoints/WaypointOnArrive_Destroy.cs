using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WaypointOnArrive_Destroy : MonoBehaviour, IWaypointOnArrive
    {
        public void OnArrival(GameObject arrivedObject)
        {
            Destroy(arrivedObject);
        }
    }
}
