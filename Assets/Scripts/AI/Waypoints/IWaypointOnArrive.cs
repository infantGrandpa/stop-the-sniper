using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public interface IWaypointOnArrive
    {
        void OnArrival(GameObject arrivedObject);
    }
}
