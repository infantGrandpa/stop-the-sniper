using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

namespace SniperProject
{

    [System.Serializable]
    public class ObjectToTilt
    {
        public Transform transform;
        public enum Axis
        {
            xAxis, yAxis, zAxis
        }
        public Axis tiltAxis;

        public Quaternion GetTiltRotation(float tiltAmount)
        {
            Quaternion tiltRotation;
            Quaternion currentRotation = transform.rotation;

            switch (tiltAxis)
            {
                case Axis.xAxis:
                    tiltRotation = Quaternion.Euler(tiltAmount, currentRotation.y, currentRotation.z);
                    break;
                case Axis.yAxis:
                    tiltRotation = Quaternion.Euler(currentRotation.x, tiltAmount, currentRotation.z);
                    break;
                default:
                    tiltRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, tiltAmount);
                    break;
            }

            return tiltRotation;
        }
    }

    public class TiltOnRotation : MonoBehaviour
    {

        [SerializeField] List<ObjectToTilt> objectsToTilt = new();

        [SerializeField] float maxTilt = 10f;

        //private float maxRotationSpeed = 45f;

        private Vector3 previousRotation;
        [SerializeField] float secsBetweenRotationCalc = 1f;

        private float targetAngle;

        private void Start()
        {
            previousRotation = transform.eulerAngles;
            targetAngle = previousRotation.z;

            StartCoroutine(GetPreviousRotationCoroutine());
        }

        private void Update()
        {
            CalculateTargetAngle();
            TiltTowardsTargetAngle();
        }

        private void CalculateTargetAngle()
        {
            float rotationDelta = (previousRotation.z - transform.eulerAngles.z) % 360;

            float rotation = rotationDelta == 0 ? 0 : Mathf.Sign(rotationDelta) * maxTilt;

            targetAngle = rotation;
        }

        private void TiltTowardsTargetAngle()
        {
            foreach (ObjectToTilt thisObject in objectsToTilt)
            {
                Vector3 newRotation = new(thisObject.transform.localEulerAngles.x, thisObject.transform.localEulerAngles.y, targetAngle);

                thisObject.transform.localEulerAngles = newRotation;
                
            }
        }

        private IEnumerator GetPreviousRotationCoroutine()
        {
            yield return new WaitForSeconds(secsBetweenRotationCalc);

            previousRotation = transform.eulerAngles;

            StartCoroutine(GetPreviousRotationCoroutine());
        }
    }
}
