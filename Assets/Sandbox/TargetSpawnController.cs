using UnityEngine;
using System.Collections.Generic;

namespace SniperProject
{
    public class TargetSpawnController : SpawnController2
    {
        public static TargetSpawnController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(TargetSpawnController)) as TargetSpawnController;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static TargetSpawnController instance;

        public List<Waypoint> waypoints = new();

        public override void LoadNewWave(WaveScriptableObject newWave)
        {
            toSpawnThisWave = newWave.soulsToSpawn;
            base.LoadNewWave(newWave);
        }

        public override void WaveComplete()
        {
            WaveManager.Instance.WaveComplete();
            base.WaveComplete();
        }

        public Waypoint GetRandomWaypoint()
        {
            if (waypoints.Count == 0)
            {
                return null;
            }

            int chosenIndex = Random.Range(0, waypoints.Count);
            return waypoints[chosenIndex];
        }

        protected override GameObject ChooseSpawnObject()
        {
            int randomIndex = Random.Range(0, currentWave.soulTypes.Count - 1);

            return currentWave.soulTypes[randomIndex];
        }

        protected override float GetSecsBeforeNextSpawn()
        {
            return currentWave.GetRandomSecsToSpawnSoul();
        }
    }
}
