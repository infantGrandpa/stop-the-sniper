using UnityEngine;

namespace SniperProject
{
    [System.Serializable]
    public class SpawnObjectData
    {
        public GameObject prefabToSpawn;
        public int toSpawnThisWave;
        private int spawnedThisWave = 0;

        public bool CanBeSpawned()
        {
            if (spawnedThisWave < toSpawnThisWave)
            {
                return true;
            }

            return false;
        }

        public void IncreaseSpawnedCount(int amount = 1)
        {
            spawnedThisWave += amount;
        }

        public void ResetSpawnedCount()
        {
            spawnedThisWave = 0;
        }
    }
}
