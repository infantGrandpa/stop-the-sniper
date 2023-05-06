using System.Collections;
using UnityEngine;


namespace SniperProject
{
    public class BulletBehaviour : MonoBehaviour
    {
        public float moveSpeed;

        public float maxRotationSpeed;
        private float currentRotationSpeed;

        [Header("Rotation Timers")]
        [SerializeField] float secsBeforeRotationAllowed;

        [SerializeField] float secsToFullRotation;
        private float secsSinceRotationAllowed;

        private bool canRotate;
        private TargetBehaviour myTarget;


        public void InitBullet(TargetBehaviour newTarget)
        {
            myTarget = newTarget;
            if (myTarget == null)
            {
                return;
            }
            myTarget.OnInvalid.AddListener(ClearTarget);

            currentRotationSpeed = 0f;
            canRotate = false;
            StartCoroutine(WaitToRotate());
        }

        private void Update()
        {           
            AttemptToRotate();
            MoveForward();
        }

        private void AttemptToRotate()
        {
            if (!canRotate)
            {
                return;
            }

            secsSinceRotationAllowed += Time.deltaTime;

            float percOfTimerCompleted = Mathf.Clamp01(secsSinceRotationAllowed / secsToFullRotation);
            currentRotationSpeed = percOfTimerCompleted * maxRotationSpeed;

            RotateTowardsTarget();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);
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

        private IEnumerator WaitToRotate()
        {
            yield return new WaitForSeconds(secsBeforeRotationAllowed);

            canRotate = true;
        }
    }
}
