using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SniperProject
{
    public class BulletBehaviour : MonoBehaviour
    {
        private TargetBehaviour myTarget;
        public float moveSpeed;
        public float maxRotationSpeed;

        public void InitBullet(TargetBehaviour newTarget)
        {
            myTarget = newTarget;
            myTarget.OnInvalid.AddListener(ClearTarget);
        }

        private void Update()
        {           
            RotateTowardsTarget();
            MoveForward();
        }

        private void RotateTowardsTarget()
        {
            if (myTarget == null)
            {
                return;
            }

            Vector2 direction = myTarget.transform.position - transform.position;
            float targetAngle = -(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg) + 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
        }

        private void MoveForward()
        {
            float movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, movementThisFrame);
        }

        private void ClearTarget()
        {
            myTarget.OnInvalid.RemoveListener(ClearTarget);
            myTarget = null;
        }

        private void OnDestroy()
        {
            if (myTarget == null)
            {
                return;
            }
            myTarget.OnInvalid.RemoveListener(ClearTarget);
        }
    }
}
