using UnityEngine;

namespace SniperProject
{
    public class IncreaseWalls : MonoBehaviour
    {
        public int increaseBy = 1;

        public void IncreaseWallsCount()
        {
            WallDeployer.Instance.AdjustWallsToDeploy(increaseBy);
            FloatingNotificationController.Instance.CreateWallsNotification(transform.position, increaseBy);
        }
    }
}
