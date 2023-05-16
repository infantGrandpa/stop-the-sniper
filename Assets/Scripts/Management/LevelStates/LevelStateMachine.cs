using UnityEngine;

namespace SniperProject
{
    public class LevelStateMachine : MonoBehaviour
    {
        private ILevelState currentState;

        private void Start()
        {
            currentState = new WaveState();
            currentState.EnterState();
        }

        private void Update()
        {
            currentState.UpdateState();
            CheckStateStatus();
        }

        private void CheckStateStatus()
        {
            if (!currentState.IsStateComplete())
            {
                return;
            }

            TransitionToNextState();
        }

        private void TransitionToNextState()
        {
            currentState.ExitState();

            switch (currentState)
            {
                case WaveState:
                    currentState = new WaveState();
                    break;
                case DeployState:
                    currentState = new DeployState();
                    break;
                default:
                    currentState = new PauseState();
                    break;
            }

            currentState.EnterState();
        }
    }
}
