using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class SpawnController : MonoBehaviour
    {
        public static SpawnController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(SpawnController)) as SpawnController;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static SpawnController instance;

        public List<SpawnerBehaviour> spawners = new();
        public List<Waypoint> waypoints = new();


        [SerializeField] float secsBetweenSpawns;
        private WaitForSeconds waitForNextSpawn;

        private Coroutine spawnCoroutine;

        private void Start()
        {
            waitForNextSpawn = new WaitForSeconds(secsBetweenSpawns);
            spawnCoroutine = StartCoroutine(WaitOnStartCoroutine());
        }

        public void PauseSpawners()
        {
            StopCoroutine(spawnCoroutine);
        }

        public void ResumeSpawners()
        {
            spawnCoroutine = StartCoroutine(WaitOnStartCoroutine());
        }

        private IEnumerator WaitOnStartCoroutine()
        {
            yield return waitForNextSpawn;
            spawnCoroutine = StartCoroutine(SpawnTargetCoroutine());
        }

        private IEnumerator SpawnTargetCoroutine()
        {

            if (spawners.Count == 0)
            {
                yield break;
            }

            bool spawnSuccess = false;
            int attempts = 0;
            int maxAttempts = spawners.Count / 2;
            while (!spawnSuccess)
            {
                if (attempts > maxAttempts)
                {
                    SpawnAtUnoccupiedSpawner();
                    break;
                }

                spawnSuccess = SpawnAtRandomSpawner();
                attempts++;

            }

            yield return waitForNextSpawn;
            spawnCoroutine = StartCoroutine(SpawnTargetCoroutine());

        }

        private bool SpawnAtRandomSpawner()
        {
            SpawnerBehaviour chosenSpawner = ChooseRandomSpawner();
            bool success = chosenSpawner.AttemptSpawnObject();
            return success;
        }

        private bool SpawnAtUnoccupiedSpawner()
        {
            SpawnerBehaviour chosenSpawner = GetFirstUnoccupiedSpawner();
            if (chosenSpawner == null)
            {
                return false;
            }

            if (!chosenSpawner.AttemptSpawnObject())
            {
                return false;
            }

            return true;
        }



        private SpawnerBehaviour ChooseRandomSpawner()
        {
            if (spawners.Count == 0)
            {
                return null;
            }

            int chosenIndex = Random.Range(0, spawners.Count);
            return spawners[chosenIndex];
        }

        private SpawnerBehaviour GetFirstUnoccupiedSpawner()
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

        public Waypoint GetRandomWaypoint()
        {
            if (waypoints.Count == 0) {
                return null;
            }

            int chosenIndex = Random.Range(0, waypoints.Count);
            return waypoints[chosenIndex];
        }
    }
}

