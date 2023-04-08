using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class RotateTowardsMouse : MonoBehaviour
    {
        [SerializeField] float maxDegreesPerSec;
        [SerializeField] float maxAngle;
        [SerializeField] float minAngle;


        void Update()
        {
            Vector3 mouseDir = GetDirectionToMouse();
            RotateTowardsDirection(mouseDir);
        }

        private Vector3 GetDirectionToMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            return direction;
        }

        private void RotateTowardsDirection(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            Quaternion maxRotation = GetMaxRotationThisFrame(angle);
            transform.rotation = maxRotation;
        }

        private Quaternion GetMaxRotationThisFrame(float targetAngle)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            Quaternion maxRotation = Quaternion.Slerp(transform.rotation, targetRotation, maxDegreesPerSec * Time.deltaTime);
            return maxRotation;
        }
    }
}