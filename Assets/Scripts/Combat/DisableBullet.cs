using UnityEngine;

namespace SniperProject
{
    public class DisableBullet : MonoBehaviour
    {

        public void DisableThisBullet()
        {
            if (TryGetComponent(out BulletBehaviour myBulletBehaviour)) {
                Destroy(myBulletBehaviour);
            }

            if (TryGetComponent(out Collider2D myCollider))
            {
                Destroy(myCollider);
            }

            if (TryGetComponent(out DamageOnCollision myDamage))
            {
                Destroy(myDamage);
            }

            if (TryGetComponent(out HealthSystem myHealthSystem))
            {
                Destroy(myHealthSystem);
            }

            if (TryGetComponent(out Rigidbody2D myRigidbody))
            {
                Destroy(myRigidbody);
            }
        }

    }
}
