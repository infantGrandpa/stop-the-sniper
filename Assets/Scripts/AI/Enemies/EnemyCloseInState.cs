using UnityEngine;

namespace SniperProject
{
    public class EnemyCloseInState : EnemyStateBase
    {
        public EnemyCloseInState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            DebugHelper.Log("Entering Close In state");
        }

        public override void UpdateState()
        {
            enemyBehaviour.SetDestinationToCurrentTarget();
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting Close In state");
        }

        public override bool IsStateComplete()
        {
            if (enemyBehaviour == null || enemyBehaviour.CurrentTarget == null)
            {
                return true;
            }

            if (enemyBehaviour.IsCurrentTargetInRange())
            {
                return true;
            }

            return false;
        }
    }
}
