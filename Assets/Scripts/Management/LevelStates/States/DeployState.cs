using UnityEngine;

namespace SniperProject
{
    public class DeployState : ILevelState
    {
        public void EnterState()
        {
            WallDeployer.Instance.InitializeNewDeployPhase();
        }
        public void UpdateState()
        {
            GetInput();
        }

        public void ExitState()
        {
            WallDeployer.Instance.ClearWallPositions();
        }

        public bool IsStateComplete()
        {
            return WallDeployer.Instance.AllWallsDeployed();
        }

        private void GetInput()
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = References.mainCanvasBehaviour.ConvertCanvasToWorld(Input.mousePosition);
                WallDeployer.Instance.DeployWallAtPosition(mousePosition);
            }
        }

    }
}
