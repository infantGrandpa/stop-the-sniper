using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SniperProject
{
    public class FloatingNotificationBehaviour : MonoBehaviour
    {
        public Image image;
        public TMP_Text text;

        public void InitializeNotification(Sprite icon, string textString, Color? color = null)
        {
            Color notificationColor = Color.white;
            if (color != null)
            {
                notificationColor = (Color)color;
            }

            image.sprite = icon;
            image.color = notificationColor;

            text.text = textString;
            text.color = notificationColor;

        }

        public void DestroyNotification()
        {
            Destroy(gameObject);
        }
    }
}
