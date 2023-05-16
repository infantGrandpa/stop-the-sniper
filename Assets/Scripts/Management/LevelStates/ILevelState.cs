using UnityEngine;
using System.Collections;

namespace SniperProject
{
    public interface ILevelState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
        bool IsStateComplete();
    }
}
