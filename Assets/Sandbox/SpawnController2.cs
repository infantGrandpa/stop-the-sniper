using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class SpawnController2 : MonoBehaviour
    {
        #region Properties and Variables
        

        public List<SpawnerBehaviour> spawners = new();
        

        protected Coroutine spawnCoroutine;

        protected WaveScriptableObject currentWave;
        [ReadOnly] public int spawnedThisWave;
        [SerializeField, ReadOnly] protected int toSpawnThisWave;

        #endregion


        #region Spawner Control
        public virtual void LoadNewWave(WaveScriptableObject newWave)
        {
            currentWave = newWave;
            spawnedThisWave = 0;
            ResumeSpawners();
        }

        public virtual void WaveComplete()
        {
            PauseSpawners();
        }

        public void PauseSpawners()
        {
            if (spawnCoroutine == null)
            {
                return;
            }

            StopCoroutine(spawnCoroutine);
        }

        public void ResumeSpawners()
        {
            spawnCoroutine = StartCoroutine(WaitOnStartCoroutine());
        }

        #endregion

        #region Spawn Helpers
        protected virtual IEnumerator WaitOnStartCoroutine()
        {
            yield return new WaitForSeconds(GetSecsBeforeNextSpawn());
            spawnCoroutine = StartCoroutine(SpawnTargetCoroutine());
        }

        protected virtual IEnumerator SpawnTargetCoroutine()
        {
            if (spawners.Count == 0)
            {
                yield break;
            }

            if (IsWaveComplete())
            {
                WaveComplete();
                yield break;
            }

            spawnedThisWave += AttemptSpawn();

            yield return new WaitForSeconds(GetSecsBeforeNextSpawn());
            spawnCoroutine = StartCoroutine(SpawnTargetCoroutine());

        }

        protected virtual int AttemptSpawn()
        {
            bool spawnSuccess = false;
            int attempts = 0;
            float maxAttemptPerc = 0.65f;
            int maxAttempts = Mathf.CeilToInt(spawners.Count * maxAttemptPerc);

            GameObject objectToSpawn = ChooseSpawnObject();
            if (objectToSpawn == null)
            {
                DebugHelper.LogWarning("Object to spawn is null.");
                return 0;
            }

            while (!spawnSuccess)
            {
                if (attempts > maxAttempts)
                {
                    spawnSuccess = SpawnAtUnoccupiedSpawner(objectToSpawn);
                    break;
                }

                spawnSuccess = SpawnAtRandomSpawner(objectToSpawn);
                attempts++;

            }

             return spawnSuccess ? 1 : 0;
        }

        protected virtual bool SpawnAtRandomSpawner(GameObject objectToSpawn)
        {
            SpawnerBehaviour chosenSpawner = ChooseRandomSpawner();

            
            bool success = chosenSpawner.AttemptSpawnObject(objectToSpawn);
            return success;
        }

        protected virtual bool SpawnAtUnoccupiedSpawner(GameObject objectToSpawn)
        {
            SpawnerBehaviour chosenSpawner = GetFirstUnoccupiedSpawner();
            if (chosenSpawner == null)
            {
                return false;
            }

            if (!chosenSpawner.AttemptSpawnObject(objectToSpawn))
            {
                return false;
            }

            return true;
        }

        protected virtual SpawnerBehaviour ChooseRandomSpawner()
        {
            if (spawners.Count == 0)
            {
                return null;
            }

            int chosenIndex = Random.Range(0, spawners.Count);
            return spawners[chosenIndex];
        }

        protected virtual SpawnerBehaviour GetFirstUnoccupiedSpawner()
        {
            if (spawners.Count == 0)
            {
                return null;
            }

            SpawnerBehaviour unoccupiedSpawner = null;
            foreach (SpawnerBehaviour thisSpawner in spawners)
            {
                if (thisSpawner.IsOccupied)
                {
                    continue;
                }

                unoccupiedSpawner = thisSpawner;
                break;
            }

            return unoccupiedSpawner;
        }


        protected virtual float GetSecsBeforeNextSpawn()
        {
            DebugHelper.LogWarning(gameObject.name + " is using the base version of GetSecsBeforeNextSpawn(), which will always return 0. Use an inherited version of the function.");
            return 0;
        }

        protected virtual GameObject ChooseSpawnObject()
        {
            DebugHelper.LogWarning(gameObject.name + " is using the base version of ChooseSpawnObject(), which will always return null. Use an inherited version of the function.");
            return null;
        }

        protected virtual bool IsWaveComplete()
        {
            if (spawnedThisWave >= toSpawnThisWave)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}

