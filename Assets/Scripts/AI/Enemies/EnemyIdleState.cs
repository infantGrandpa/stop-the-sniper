using UnityEngine;

namespace SniperProject
{
    public class EnemyIdleState : EnemyStateBase
    {
        public EnemyIdleState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            DebugHelper.Log("Entering Idle state");
        }

        public override void UpdateState()
        {
            
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting Idle state");
        }

        public override bool IsStateComplete()
        {
            if (enemyBehaviour == null)
            {
                return true;
            }

            if (enemyBehaviour.CurrentTarget != null)
            {
                return true;
            }

            return false;
        }
    }
}
