using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(WaveManager)) as WaveManager;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static WaveManager instance;

        [SerializeField] float secsBetweenWaves;
        [SerializeField] List<WaveScriptableObject> listOfWaves = new();
        private int currentWaveIndex = -1;
        private WaveScriptableObject currentWave;

        public int SoulsAscendedCount { get; private set; }
        public int SoulsLostCount { get; private set; }

        private int totalSoulsAscended;
        private int totalSoulsLost;


        private void Start()
        {
            if (listOfWaves.Count == 0)
            {
                Debug.LogError("ERROR WaveManager Start(): No assigned waves.");
            }

            GetNextWave();
        }

        private void GetNextWave()
        {
            currentWaveIndex++;

            if (currentWaveIndex > listOfWaves.Count - 1)
            {
                Win();
                return;
            }

            StartNewWave();
        }

        private void StartNewWave()
        {
            currentWave = listOfWaves[currentWaveIndex];

            SoulsAscendedCount = 0;
            SoulsLostCount = 0;

            References.mainCanvasBehaviour.UpdateScoreCounter();
            References.mainCanvasBehaviour.UpdateWave(currentWaveIndex);

            StartCoroutine(WaitForWaveBreakCoroutine());
        }

        public void IncreaseAscendedSouls(int increaseBy = 1)
        {
            SoulsAscendedCount += increaseBy;
            totalSoulsAscended += increaseBy;
            References.mainCanvasBehaviour.UpdateScoreCounter();

            if (SoulsAscendedCount >= currentWave.ascensionsToProgress)
            {
                CompleteWave();
            }
        }

        public void IncreaseLostSouls(int increaseBy = 1)
        {
            SoulsLostCount += increaseBy;
            totalSoulsLost += increaseBy;
            References.mainCanvasBehaviour.UpdateScoreCounter();

            if (SoulsLostCount >= currentWave.lossesToFail)
            {
                FailWave();
            }
        }

        private void CompleteWave()
        {
            Debug.Log("Completed wave " + currentWaveIndex + "!");
            GetNextWave();
            SpawnController.Instance.PauseSpawners();
        }

        private void FailWave()
        {
            Debug.Log("Failed wave " + currentWaveIndex + ".");
            GetNextWave();
            SpawnController.Instance.PauseSpawners();
        }

        private void Win()
        {
            Debug.Log("Won! Resetting...");
            currentWaveIndex = -1;
            GetNextWave();
        }

        private IEnumerator WaitForWaveBreakCoroutine()
        {
            bool targetsOnField = LevelManager.Instance.targets.Count > 0;
            while (targetsOnField)
            {
                yield return null;
            }

            yield return new WaitForSeconds(secsBetweenWaves);
            SpawnController.Instance.ResumeSpawners();
        }


    }
}
