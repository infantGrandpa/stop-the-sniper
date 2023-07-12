using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class CanisterSpawnController2 : SpawnController2
    {
        public static CanisterSpawnController2 Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(CanisterSpawnController2)) as CanisterSpawnController2;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static CanisterSpawnController2 instance;

        [ReadOnly]
        public List<CanisterBehaviour> activeCanisters = new();

        public override void LoadNewWave(WaveScriptableObject newWave)
        {
            toSpawnThisWave = newWave.canistersToSpawn;
            base.LoadNewWave(newWave);
        }

        protected override GameObject ChooseSpawnObject()
        {
            int randomIndex = Random.Range(0, currentWave.canisterTypes.Count - 1);

            return currentWave.canisterTypes[randomIndex];
        }

        protected override float GetSecsBeforeNextSpawn()
        {
            return currentWave.GetRandomSecsToSpawnSoul();
        }

        public void DestroyAllCanisters()
        {
            foreach (CanisterBehaviour thisCanister in activeCanisters)
            {
                thisCanister.DestroyCanisterAtEndOfWave();
            }
        }

        protected override int AttemptSpawn()
        {
            DebugHelper.Log("Attempting to Spawn...");
            return base.AttemptSpawn();
        }

        //Hack. Gets called at the end of each wave.
        public void UnoccupyAllSpawners()
        {
            foreach (SpawnerBehaviour thisSpawner in spawners)
            {
                thisSpawner.MarkUnoccupied();
            }
        }
    }
}
