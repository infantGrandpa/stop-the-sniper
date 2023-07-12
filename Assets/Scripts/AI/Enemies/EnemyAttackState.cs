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

            enemyBehaviour.MatchTargetSpeed();
        }

        public override void UpdateState()
        {
            enemyBehaviour.SetDestinationToCurrentTarget();
        }

        public override void ExitState()
        {
            enemyBehaviour.ResetSpeed();
        }

        public override bool IsStateComplete()
        {
            if (enemyBehaviour == null || enemyBehaviour.CurrentTarget == null)
            {
                return true;
            }

            if (!enemyBehaviour.IsCurrentTargetInRange())
            {
                return true;
            }

            return false;
        }
    }
}
