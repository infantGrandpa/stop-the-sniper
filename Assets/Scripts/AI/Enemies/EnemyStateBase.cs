using UnityEngine;

namespace SniperProject
{
    [System.Serializable]
    public abstract class EnemyStateBase
    {
        protected EnemyBehaviour enemyBehaviour;
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract bool IsStateComplete();
        //public abstract EnemyStateBase GetNextState();
    }
}
