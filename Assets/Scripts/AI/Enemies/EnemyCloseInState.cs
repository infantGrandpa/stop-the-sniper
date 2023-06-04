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

            Vector2 currentPosition = enemyBehaviour.transform.position;
            Vector2 targetPosition = enemyBehaviour.CurrentTarget.transform.position;
            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

            if (distanceToTarget <= enemyBehaviour.attackRange)
            {
                return true;
            }

            return false;
        }
    }
}
