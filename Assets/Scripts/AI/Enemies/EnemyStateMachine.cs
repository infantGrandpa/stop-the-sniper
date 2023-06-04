using UnityEngine;

namespace SniperProject
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyBehaviour enemyBehaviour;

        private EnemyStateBase currentState;

        private void Start()
        {
            TransitionToNextState();
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
            if (enemyBehaviour.CurrentTarget == null)
            {
                currentState = new EnemyIdleState(enemyBehaviour);
                return;
            }


        }
    }
}
