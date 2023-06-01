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
            WallDeployer.Instance.EndDeployPhase();
        }

        public bool IsStateComplete()
        {
            return WallDeployer.Instance.AllWallsDeployed();
        }

        private void GetInput()
        {
            Vector2 mousePosition = MainCanvasBehaviour.Instance.ConvertCanvasToWorld(Input.mousePosition);
            WallDeployer.Instance.UpdateGhostPosition(mousePosition);

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                WallDeployer.Instance.DeployWallAtPosition(mousePosition);
            }
        }

    }
}
