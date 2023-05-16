using UnityEngine;

namespace SniperProject
{
    public class TransitionState : ILevelState
    {
        public ILevelState nextState;
        public float secsBeforeTransitionComplete;
        private float secsSinceEnter;

        public void EnterState()
        {
            secsSinceEnter = 0;
        }
        public void UpdateState()
        {
            secsSinceEnter += Time.deltaTime;
        }

        public void ExitState()
        {
            
        }

        public bool IsStateComplete()
        {
            bool stateComplete = secsSinceEnter >= secsBeforeTransitionComplete;
            return stateComplete;
        }

    }
}
