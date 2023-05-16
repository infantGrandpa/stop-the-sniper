using UnityEngine;

namespace SniperProject
{
    public class PauseState : ILevelState
    {
        public void EnterState()
        {
            Debug.Log("Entering Pause State");
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            Debug.Log("Exit Pause State");
        }

        public bool IsStateComplete()
        {
            return true;
        }

    }
}
