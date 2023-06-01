using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

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

        [SerializeField] Transform point1Transform;
        [SerializeField] Transform point2Transform;

        private bool showGhostWall;
        private WallBehaviour currentWallBehaviour;
        private Vector2 currentGhostPosition;

        public void DeployWallAtPosition(Vector2 positionToDeploy)
        {
            if (AllWallsDeployed() || currentWallBehaviour == null)
            {
                return;
            }

            UpdateGhostPosition(positionToDeploy);

            if (!IsWallPositionValid())
            {
                return;
            }

            ConvertGhostToWall();
            GetNextWall();
        }

        public bool AllWallsDeployed()
        {
            bool allWallsDeployed = wallsDeployed >= wallsToDeploy;
            return  allWallsDeployed;
        }

        private bool IsWallPositionValid()
        {
            return currentWallBehaviour.IsCurrentPositionValid();
        }

        //May not need this anymore, but will keep around for the time being
        private bool IsPathThroughWallsPossible(Vector2 positionToTest)
        {
            if (AstarPath.active == null)
            {
                return false;
            }

            Vector3 point1 = point1Transform.position;
            Vector3 point2 = point2Transform.position;

            GraphNode node1 = AstarPath.active.GetNearest(point1, NNConstraint.Default).node;
            GraphNode node2 = AstarPath.active.GetNearest(point2, NNConstraint.Default).node;

            if (PathUtilities.IsPathPossible(node1, node2))
            {
                return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            if (!showGhostWall)
            {
                return;
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(currentGhostPosition, 0.5f);
            
        }

        public void EndDeployPhase()
        {
            HideGhost();
        }

        public void InitializeNewDeployPhase()
        {
            wallsDeployed = 0;
            GetNextWall();
            ShowGhost();
        }

        private void ShowGhost()
        {
            showGhostWall = true;
        }

        public void UpdateGhostPosition(Vector2 newPosition)
        {
            if (currentWallBehaviour == null)
            {
                return;
            }

            currentWallBehaviour.UpdateGhostPosition(newPosition);
            currentGhostPosition = newPosition;
        }

        private void HideGhost()
        {
            showGhostWall = false;
        }

        private void GetNextWall()
        {
            if (AllWallsDeployed())
            {
                currentWallBehaviour = null;
                return;
            }

            GameObject wallInstance = LevelManager.Instance.InstantiateObjectOnDyanmicTransform(wallPrefab);
            if (!wallInstance.TryGetComponent(out currentWallBehaviour))
            {
                DebugHelper.LogError("Wall Prefab(" + wallPrefab.name + ") is missing a WallBehaviour component.");
                return;
            }

            currentWallBehaviour.InitializeAsGhost();
        }

        private void ConvertGhostToWall()
        {
            currentWallBehaviour.InitializeAsWall();
            wallsDeployed++;
        }


    }
}
