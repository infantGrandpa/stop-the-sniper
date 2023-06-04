using UnityEngine;

namespace SniperProject
{
    public class IncreaseWalls : MonoBehaviour
    {
        public void IncreaseWallsBy1()
        {
            IncreaseDeployableWalls();
        }

        public void IncreaseDeployableWalls(int increaseBy = 1)
        {
            WallDeployer.Instance.AdjustWallsToDeploy(increaseBy);
            FloatingNotificationController.Instance.CreateWallsNotification(transform.position, increaseBy);
        }
    }
}
