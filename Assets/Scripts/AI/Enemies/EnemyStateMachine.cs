using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    [RequireComponent(typeof(EnemyBehaviour))]
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyBehaviour enemyBehaviour;
        private EnemyStateBase currentState;


        [Header("For Debugging")]
        [SerializeField, ReadOnly] string currentStateName = "None";


        private void Awake()
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
        }


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
            currentStateName = currentState?.GetType().Name ?? "None";

            if (currentState == null)
            {
                return;
            }

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

            if (enemyBehaviour.IsCurrentTargetInRange())
            {
                currentState = new EnemyAttackState(enemyBehaviour);
                return;
            }

            currentState = new EnemyCloseInState(enemyBehaviour);
        }
    }
}
