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

            StartCoroutine(WaveBreakCoroutine());
        }

        private void StartNewWave()
        {
            currentWave = listOfWaves[currentWaveIndex];

            SoulsAscendedCount = 0;
            SoulsLostCount = 0;

            SpawnController.Instance.LoadNewWave(currentWave);
        }

        public void IncreaseAscendedSouls(int increaseBy = 1)
        {
            SoulsAscendedCount += increaseBy;
            totalSoulsAscended += increaseBy;
            References.mainCanvasBehaviour.UpdateScoreCounter();
        }

        public void IncreaseLostSouls(int increaseBy = 1)
        {
            SoulsLostCount += increaseBy;
            totalSoulsLost += increaseBy;
            References.mainCanvasBehaviour.UpdateScoreCounter();
        }

        private float GetSoulsAscendedPercentage()
        {
            int soulsNotLost = currentWave.soulsToSpawn - SoulsLostCount;
            float soulsPerc = (float)soulsNotLost / (float)currentWave.soulsToSpawn;
            Debug.Log("SAC (" + soulsNotLost.ToString() + ") / STS (" + currentWave.soulsToSpawn.ToString() + ") = Perc (" + soulsPerc.ToString() + ")");
            return soulsPerc;
        }

        public void WaveComplete()
        {
            StartCoroutine(WaitForTargetsToDisappearCoroutine());
        }

        private void Win()
        {
            Debug.Log("Won! Resetting...");
            currentWaveIndex = -1;
            GetNextWave();
        }

        private IEnumerator WaitForTargetsToDisappearCoroutine()
        {
            bool targetsOnField = LevelManager.Instance.targets.Count > 0;
            while (targetsOnField)
            {
                targetsOnField = LevelManager.Instance.targets.Count > 0;
                yield return null;
            }

            float soulsAscendedPercentage = GetSoulsAscendedPercentage();

            string successMsg = "SUCCESS: ";
            if (soulsAscendedPercentage < currentWave.ascensionRatioToWin)
            {
                successMsg = "FAILED: ";
            }
            Debug.Log("Wave " + currentWaveIndex + " completed. " + successMsg + (soulsAscendedPercentage * 100).ToString() + "%");
            GetNextWave();
        }

        private IEnumerator WaveBreakCoroutine()
        {
            References.mainCanvasBehaviour.UpdateScoreCounter();
            References.mainCanvasBehaviour.UpdateWave(currentWaveIndex);

            yield return new WaitForSeconds(secsBetweenWaves);
            StartNewWave();
        }


    }
}
