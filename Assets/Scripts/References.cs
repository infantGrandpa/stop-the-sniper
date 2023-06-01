using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public static class References
    {
        public static float maxDistanceInLevel = 500f;

        //Layer Masks
        public static int targetLayerMaskInt = LayerMask.GetMask("Targets");
        public static int obstacleLayerMaskInt = LayerMask.GetMask("Obstacles");
        public static int nonCollidingObjectsLayerMaskInt = LayerMask.GetMask("NonCollidingObjects");

        //Layers
        public static int targetLayerInt = LayerMask.NameToLayer("Targets");
        public static int obstacleLayerInt = LayerMask.NameToLayer("Obstacles");
        public static int nonCollidingObjectsLayerInt = LayerMask.NameToLayer("NonCollidingObjects");
    }
}
