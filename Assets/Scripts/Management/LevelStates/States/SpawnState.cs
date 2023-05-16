using System.Collections;
using UnityEngine;

namespace SniperProject
{
    public class SpawnState : ILevelState
    {

        public void EnterState()
        {
            AstarPath.active.Scan();
            WaveManager.Instance.GetNextWave();
            Debug.Log("Entering Wave " + WaveManager.Instance.CurrentWaveIndex);
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            Debug.Log("Exiting Wave " + WaveManager.Instance.CurrentWaveIndex);
        }

        public bool IsStateComplete()
        {
            return WaveManager.Instance.IsWaveComplete;
        }

    }
}
