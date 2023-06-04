using UnityEngine;

namespace SniperProject
{
    public class IncreaseLost : MonoBehaviour
    {
        public void IncreaseLostSoulCountOnDeath()
        {
            WaveManager.Instance.IncreaseLostSouls();
        }

        public void IncreaseLostSoulsBy1()
        {
            IncreaseLostSouls();
        }

        public void IncreaseLostSouls(int increaseBy = 1)
        {
            WaveManager.Instance.IncreaseLostSouls(increaseBy);
            FloatingNotificationController.Instance.CreateLostNotification(transform.position, increaseBy);
        }
    }
}
