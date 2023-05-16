using System.Collections;
using UnityEngine;

namespace SniperProject
{
    public class WaveState : ILevelState
    {
        public int maxEnemies = 3;
        private float enemiesSpawned = 0f;

        public void EnterState()
        {
            enemiesSpawned = 0;
            Debug.Log("Entering Wave " + WaveManager.Instance.CurrentWaveIndex);
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            Debug.Log("Exiting Wave " + WaveManager.Instance.CurrentWaveIndex);
        }

        private void SpawnEnemy()
        {
            enemiesSpawned += Time.deltaTime;
            if (enemiesSpawned >= maxEnemies)
            {
                ExitState();
            }
        }

        public bool IsStateComplete()
        {
            return WaveManager.Instance.IsWaveComplete;
        }

    }
}
