using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class CanisterSpawnController : SpawnController2
    {
        [ReadOnly]
        public List<CanisterBehaviour> activeCanisters = new();

        public static CanisterSpawnController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(CanisterSpawnController)) as CanisterSpawnController;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static CanisterSpawnController instance;

        public override void LoadNewWave(WaveScriptableObject newWave)
        {
            base.LoadNewWave(newWave);
            toSpawnThisWave = newWave.GetCanisterCountData();

            foreach (SpawnObjectData thisObject in currentWave.canisterData)
            {
                thisObject.ResetSpawnedCount();
            }
        }


        protected override GameObject ChooseSpawnObject()
        {
            List<SpawnObjectData> spawnableObjects = new();

            //Create list of Canisters that are allowed to spawn
            foreach (SpawnObjectData thisObject in currentWave.canisterData)
            {
                if (!thisObject.CanBeSpawned())
                {
                    continue;
                }

                spawnableObjects.Add(thisObject);
            }

            if (spawnableObjects.Count <= 0)
            {
                WaveComplete();
                return null;
            }

            int randomIndex = Random.Range(0, spawnableObjects.Count);
            SpawnObjectData objectToSpawn = spawnableObjects[randomIndex];
            objectToSpawn.IncreaseSpawnedCount();

            if (objectToSpawn == null)
            {
                return null;        //For testing purposes
            }

            return objectToSpawn.prefabToSpawn;
        }

        protected override float GetSecsBeforeNextSpawn()
        {
            return currentWave.GetRandomSecsToSpawnCanister();
        }

        public void DestroyAllCanisters()
        {
            foreach (CanisterBehaviour thisCanister in activeCanisters)
            {
                thisCanister.DestroyCanisterAtEndOfWave();
            }
        }
    }
}
