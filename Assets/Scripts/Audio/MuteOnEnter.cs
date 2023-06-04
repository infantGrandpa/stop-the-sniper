using UnityEngine;

namespace AudioManager
{
    public class MuteOnEnter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Entering trigger...");
            if (!collision.gameObject.TryGetComponent(out PlaySound playSoundComponent)) 
            {
                Debug.Log("No sound component.");
                return;
            }

            Debug.Log("Muting...");
            playSoundComponent.Mute();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Entering trigger...");

            if (!collision.gameObject.TryGetComponent(out PlaySound playSoundComponent))
            {
                Debug.Log("No sound component.");
                return;
            }

            Debug.Log("Muting...");
            playSoundComponent.Mute();
        }
    }
}
