using UnityEngine;

namespace SniperProject
{
    public class DisableBullet : MonoBehaviour
    {

        public void DisableThisBullet()
        {
            if (TryGetComponent(out RotatingBulletBehaviour myBulletBehaviour)) {
                float moveSpeed = myBulletBehaviour.moveSpeed;
                Destroy(myBulletBehaviour);
                BulletBehaviour newBulletBehaviour = gameObject.AddComponent<BulletBehaviour>(); //Keeps the bullet moving forward
                newBulletBehaviour.moveSpeed = moveSpeed;
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
