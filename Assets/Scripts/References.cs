using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public static class References
    {
        public static float maxDistanceInLevel = 500f;

        public static MainCanvasBehaviour mainCanvasBehaviour;

        public static int targetLayerMaskInt = LayerMask.GetMask("Targets");
    }
}
