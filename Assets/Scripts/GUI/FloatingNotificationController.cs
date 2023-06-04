using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class FloatingNotificationController : MonoBehaviour
    {
        public static FloatingNotificationController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(FloatingNotificationController)) as FloatingNotificationController;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static FloatingNotificationController _instance;
        
        [ValidateInput("HasNotificationBehaviour", "The notification prefab must have FloatingNotificationBehaviour component.")]
        [SerializeField] GameObject notificationPrefab;

        [SerializeField, BoxGroup("Escaped Ships")] Sprite escapedIcon;
        [SerializeField, BoxGroup("Escaped Ships")] Color escapedColor;
        
        [SerializeField, BoxGroup("Lost Ships")] Sprite lostIcon;
        [SerializeField, BoxGroup("Lost Ships")] Color lostColor;

        [SerializeField, BoxGroup("Walls")] Sprite wallIcon;
        [SerializeField, BoxGroup("Walls")] Color wallColor;

        public void CreateEscapedNotification(Vector2 worldPosition, int number)
        {
            CreateNewNotification(worldPosition, escapedIcon, number, escapedColor);
        }

        public void CreateLostNotification(Vector2 worldPosition, int number)
        {
            CreateNewNotification(worldPosition, lostIcon, number, lostColor);
        }

        public void CreateWallsNotification(Vector2 worldPosition, int number)
        {
            CreateNewNotification(worldPosition, wallIcon, number, wallColor);
        }

        private void CreateNewNotification(Vector2 worldPosition, Sprite icon, int number, Color color)
        {
            if (icon == null || color == null)
            {
                DebugHelper.LogError("New Notification was provided null icon or color.");
                return;
            }

            string text = GetTextString(number);

            GameObject newNotification = Instantiate(notificationPrefab);
            newNotification.transform.SetParent(transform);
            newNotification.transform.position = MainCanvasBehaviour.Instance.ConvertWorldToCanvas(worldPosition);

            if (!newNotification.TryGetComponent(out FloatingNotificationBehaviour notificationBehaviour))
            {
                DebugHelper.LogError(notificationPrefab.name + " is missing a FloatingNotificationBehaviour component.");
            }

            notificationBehaviour.InitializeNotification(icon, text, color);
        }

        private string GetTextString(int number)
        {
            string sign = "+";
            if (number < 0)
            {
                sign = "-";
            }

            string finalString = sign + number.ToString();
            return finalString;
        }

        private bool HasNotificationBehaviour(GameObject gameObjectToTest)
        {
            if (gameObjectToTest == null)
            {
                return true;
            }

            if (gameObjectToTest.GetComponent<FloatingNotificationBehaviour>() != null)
            {
                return true;
            }

            return false;
        }

    }
}
