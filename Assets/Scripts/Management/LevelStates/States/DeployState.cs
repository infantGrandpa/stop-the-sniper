using UnityEngine;

namespace SniperProject
{
    public class DeployState : ILevelState
    {
        public void EnterState()
        {
            Debug.Log("Entering Deploy State");
        }
        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            Debug.Log("Exiting Deploy State");
        }

        public bool IsStateComplete()
        {
            return false;
        }

    }
}
