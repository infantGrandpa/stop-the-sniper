using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class WaypointOnArrive_Ascend : MonoBehaviour, IWaypointOnArrive
    {
        [SerializeField] Vector2 notificationPosition;
        [SerializeField] UnityEvent onArriveEvent;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (notificationPosition == Vector2.zero)
            {
                ResetNotificationPosition();
            }

        }

        public void OnArrival(GameObject arrivedObject)
        {
            WaveManager.Instance.IncreaseAscendedSouls();
            FloatingNotificationController.Instance.CreateEscapedNotification(notificationPosition, 1);

            onArriveEvent?.Invoke();

            Destroy(arrivedObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(notificationPosition, Vector3.one);
        }

        [ContextMenu("Reset Notification Position")]
        private void ResetNotificationPosition()
        {
            notificationPosition = (Vector2)transform.position;
        }
    }
}
