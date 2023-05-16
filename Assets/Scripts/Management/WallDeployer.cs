using UnityEngine;
using System.Collections.Generic;

namespace SniperProject
{
    public class WallDeployer : MonoBehaviour
    {
        public static WallDeployer Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(WallDeployer)) as WallDeployer;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static WallDeployer instance;

        [SerializeField] GameObject wallPrefab;
        public int wallsToDeploy = 3;
        private int wallsDeployed;

        private List<Vector2> wallPositions = new();

        public void DeployWallAtPosition(Vector2 positionToDeploy)
        {
            if (wallsDeployed >= wallsToDeploy)
            {
                return;
            }

            Debug.Log("Deploying wall at " + positionToDeploy.ToString());

            GameObject wallInstance = LevelManager.Instance.InstantiateObjectOnDyanmicTransform(wallPrefab);
            wallInstance.transform.position = positionToDeploy;

            wallPositions.Add(positionToDeploy);
            wallsDeployed++;
        }

        public bool AllWallsDeployed()
        {
            bool allWallsDeployed = wallsDeployed >= wallsToDeploy;
            return  allWallsDeployed;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach(Vector2 thisPosition in wallPositions)
            {
                Gizmos.DrawWireSphere(thisPosition, 0.5f);
            }
        }

        public void ClearWallPositions()
        {
            wallPositions.Clear();
            wallsDeployed = 0;
        }

        public void InitializeNewDeployPhase()
        {
            wallsDeployed = 0;
        }

    }
}
