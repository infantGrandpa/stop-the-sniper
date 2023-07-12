using UnityEngine;

namespace SniperProject
{
    public class DeployState : ILevelState
    {
        public void EnterState()
        {
            WallDeployer.Instance.InitializeNewDeployPhase();
            TargetReticleController.Instance.HideReticle();
        }
        public void UpdateState()
        {
            GetInput();
        }

        public void ExitState()
        {
            TargetReticleController.Instance.ShowReticle();
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
