using UnityEngine;

namespace SniperProject
{
    public class BulletBehaviour : MonoBehaviour
    {
        public float moveSpeed;

        protected virtual void Update()
        {
            MoveForward();
        }

        public virtual void InitBullet(TargetBehaviour newTarget = null)
        {
            //Do nothing
        }

        protected virtual void MoveForward()
        {
            float movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, movementThisFrame);
        }
    }
}
