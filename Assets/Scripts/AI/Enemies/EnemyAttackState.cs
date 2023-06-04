using UnityEngine;

namespace SniperProject
{
    public class EnemyAttackState : EnemyStateBase
    {
        public EnemyAttackState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            DebugHelper.Log("Entering Attack state");
        }

        public override void UpdateState()
        {
            
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting Attack state");
        }

        public override bool IsStateComplete()
        {
            if (enemyBehaviour == null)
            {
                return true;
            }

            if (enemyBehaviour.CurrentTarget == null)
            {
                return true;
            }

            return false;
        }
    }
}
