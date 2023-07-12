using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SniperProject
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        [BoxGroup("Soul Spawning"), OnValueChanged("UpdateRangeToSpawnAllSouls"), 
            ValidateInput("ValidateGreaterThan0", "Souls to Spawn must be greater than 0", InfoMessageType.Error)]
        public int soulsToSpawn = 5;
        [BoxGroup("Soul Spawning"), OnValueChanged("UpdateRangeToSpawnAllSouls"), MinMaxSlider(0, 10f, true), Space]
        public Vector2 secsBetweenSoulSpawns = new(0, 5);

        [BoxGroup("Soul Spawning"), Space, ReadOnly]
        [SerializeField] Vector2 rangeToSpawnAllSouls;

        [BoxGroup("Soul Spawning"), Range(0, 1), Space]
        public float ascensionRatioToWin = 0.6f;
        [BoxGroup("Soul Spawning"), Space, AssetsOnly]
        public List<GameObject> soulTypes = new();

        [BoxGroup("Canister Spawning"), OnValueChanged("UpdateCanisterEditorInfo")]
        public List<SpawnObjectData> canisterData = new();
        [BoxGroup("Canister Spawning"), OnValueChanged("UpdateCanisterEditorInfo"), MinMaxSlider(0, 10f, true), Space]
        public Vector2 secsBetweenCanisterSpawns = new(0, 5);

        [Button(ButtonSizes.Large)]
        private void RefreshData()
        {
            UpdateRangeToSpawnAllSouls();
            UpdateCanisterEditorInfo();
        }

        [BoxGroup("Canister Spawning"), Space, DisplayAsString,
            InfoBox("The maximum time to spawn all canisters should be less than the minimum time to spawn all souls.")]
        [SerializeField] Vector2 rangeToSpawnAllCanisters;

        [BoxGroup("Canister Spawning")]
        [SerializeField, ReadOnly] int totalCanistersToSpawn;
        [BoxGroup("Canister Spawning")]
        [SerializeField, ReadOnly] int totalAsteroidsToSpawn;


        [BoxGroup("Hacky Canister Spawning")]
        public int canistersToSpawn = 5;
        [BoxGroup("Hacky Canister Spawning"), AssetsOnly]
        public List<GameObject> canisterTypes = new();
        [BoxGroup("Hacky Canister Spawning")]
        public Vector2 secsBetweenCanisters = new(0, 5);


        public int GetCanisterCountData()
        {
            int totalCanisters = 0;
            foreach (SpawnObjectData thisCanisterType in canisterData)
            {
                totalCanisters += thisCanisterType.toSpawnThisWave;
            }

            return totalCanisters;
        }

        public int GetTotalAsteroids()
        {
            int totalAsteroids = 0;
            foreach (SpawnObjectData thisCanisterType in canisterData)
            {
                int asteroidsSpawnedByThisCanister = GetAsteroidsSpawnedByCanister(thisCanisterType);
                totalAsteroids += asteroidsSpawnedByThisCanister * thisCanisterType.toSpawnThisWave;
            }

            return totalAsteroids;
        }

        public float GetRandomSecsToSpawnSoul()
        {
            return Random.Range(secsBetweenSoulSpawns.x, secsBetweenSoulSpawns.y);
        }

        public float GetRandomSecsToSpawnCanister()
        {
            return Random.Range(secsBetweenCanisterSpawns.x, secsBetweenCanisterSpawns.y);
        }

        private void UpdateRangeToSpawnAllSouls()
        {
            rangeToSpawnAllSouls.x = secsBetweenSoulSpawns.x * soulsToSpawn;
            rangeToSpawnAllSouls.y = secsBetweenSoulSpawns.y * soulsToSpawn;
        }

        private void UpdateCanisterEditorInfo()
        {
            totalCanistersToSpawn = GetCanisterCountData();
            totalAsteroidsToSpawn = GetTotalAsteroids();
            rangeToSpawnAllCanisters.x = secsBetweenCanisterSpawns.x * totalCanistersToSpawn;
            rangeToSpawnAllCanisters.y = secsBetweenCanisterSpawns.y * totalCanistersToSpawn;
        }

        private int GetAsteroidsSpawnedByCanister(SpawnObjectData canister)
        {
            GameObject canisterObject = canister.prefabToSpawn;
            if (!canisterObject.TryGetComponent(out IncreaseWalls increaseWalls)) {
                return 0;
            }

            return increaseWalls.increaseBy;
        }

        private bool ValidateGreaterThan0(int value)
        {
            return value > 0;
        }
    }
}
