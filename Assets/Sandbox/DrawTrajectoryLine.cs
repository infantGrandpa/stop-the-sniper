using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    [RequireComponent(typeof (HomingSignal))]
    [RequireComponent(typeof(WeaponBehaviour))]
    public class DrawTrajectoryLine : MonoBehaviour
    {
        private HomingSignal homingSignal;
        private WeaponBehaviour weapon;
        private BulletBehaviour bulletBehaviour;
        [SerializeField] GameObject trajectoryPoints;


        private void Start()
        {
            homingSignal = GetComponent<HomingSignal>();
            weapon = GetComponent<WeaponBehaviour>();
            bulletBehaviour = weapon.bulletPrefab.GetComponent<BulletBehaviour>();
        }

        private void Update()
        {
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            TargetBehaviour currentTarget = homingSignal.CurrentTarget;

            if (currentTarget == null)
            {
                return;
            }

            Vector2 direction = currentTarget.transform.position - transform.position;
            float targetAngle = -(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg) + 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, bulletBehaviour.maxRotationSpeed * Time.deltaTime);

            // Calculate the trajectory points
            int numPoints = trajectoryPoints.transform.childCount;
            Vector3[] points = new Vector3[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                float t = (float)i / (float)(numPoints - 1);
                Vector3 point = CalculatePointAlongCurve(t);
                points[i] = point;
                trajectoryPoints.transform.GetChild(i).position = point;
            }

            // Update the line renderer
            LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = numPoints;
            lineRenderer.SetPositions(points);
        }

        private Vector3 CalculatePointAlongCurve(float t)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = homingSignal.CurrentTarget.transform.position;
            Vector3 startVelocity = transform.right * bulletBehaviour.moveSpeed;
            

            Vector2 direction = endPosition - startPosition;
            float distance = Vector2.Distance(startPosition, endPosition);
            float secsToTarget = distance / bulletBehaviour.moveSpeed;

            float height = direction.y;

            Vector3 velocityXY = direction / secsToTarget;
            velocityXY.y = startVelocity.y;

            Vector3 velocityX = Vector3.right * Mathf.Sqrt(-2f * height);
            float timeZ = Mathf.Sqrt(-2f * height);

            Vector3 velocity = velocityXY + velocityX;
            Vector3 point = startPosition + t * (secsToTarget * velocityXY + 0.5f * timeZ * velocityX);

            return point;
        }





        //private float collisionCheckRadius = 0.1f;
        //private void SimulateArc()
        //{
        //    float simulateForDuration = 5f;//simulate for 5 secs in the furture
        //    float simulationStep = 0.1f;//Will add a point every 0.1 secs.

        //    int steps = (int)(simulateForDuration / simulationStep);//50 in this example
        //    List<Vector2> lineRendererPoints = new List<Vector2>();
        //    Vector2 calculatedPosition;
        //    Vector2 directionVector = new Vector2(0.5f, 0.5f);//You plug you own direction here this is just an example
        //    Vector2 launchPosition = Vector2.zero;//Position where you launch from
        //    float launchSpeed = 10f;//Example speed per secs.

        //    for (int i = 0; i < steps; ++i)
        //    {
        //        calculatedPosition = launchPosition + (directionVector * (launchSpeed * i * simulationStep));
        //        //Calculate gravity
        //        calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep) * (i * simulationStep);
        //        lineRendererPoints.Add(calculatedPosition);
        //        if (CheckForCollision(calculatedPosition))//if you hit something
        //        {
        //            break;//stop adding positions
        //        }
        //    }

        //    //Assign all the positions to the line renderer.
        //}
        //private bool CheckForCollision(Vector2 position)
        //{
        //    Collider2D[] hits = Physics2D.OverlapCircleAll(position, collisionCheckRadius);
        //    if (hits.Length > 0)
        //    {
        //        //We hit something 
        //        //check if its a wall or seomthing
        //        //if its a valid hit then return true
        //        return true;
        //    }
        //    return false;
        //}
    }
}
