using UnityEngine;

namespace SniperProject
{
    public interface IEnemyState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
        bool IsStateComplete();
    }
}
