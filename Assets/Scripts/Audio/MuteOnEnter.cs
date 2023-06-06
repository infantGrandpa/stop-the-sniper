using UnityEngine;

namespace AudioManager
{
    public class MuteOnEnter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out PlaySound playSoundComponent)) 
            {
                return;
            }

            playSoundComponent.Mute();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out PlaySound playSoundComponent))
            {
                return;
            }

            playSoundComponent.Mute();
        }
    }
}
