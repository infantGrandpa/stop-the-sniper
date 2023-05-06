using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WaypointOnArrive_Ascend : MonoBehaviour, IWaypointOnArrive
    {
        public void OnArrival(GameObject arrivedObject)
        {
            WaveManager.Instance.IncreaseAscendedSouls();
            Destroy(arrivedObject);
        }
    }
}
