using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WaveManager : MonoBehaviour
    {
        #region Properties and Variables

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

        public float secsBetweenWaves;
        [SerializeField] List<WaveScriptableObject> listOfWaves = new();
        public int CurrentWaveIndex { get; private set; }
        private WaveScriptableObject currentWave;

        public int SoulsAscendedCount { get; private set; }
        public int SoulsLostCount { get; private set; }

        public bool IsWaveComplete { get; private set; }

        private int totalSoulsAscended;
        private int totalSoulsLost;

        #endregion

        #region Start

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            CurrentWaveIndex = -1;
            if (listOfWaves.Count == 0)
            {
                Debug.LogError("ERROR WaveManager Start(): No assigned waves.");
            }
        }

        public void GetNextWave()
        {
            CurrentWaveIndex++;

            if (CurrentWaveIndex > listOfWaves.Count - 1)
            {
                Win();
                return;
            }

            StartCoroutine(WaveBreakCoroutine());
        }

        private void StartNewWave()
        {
            currentWave = listOfWaves[CurrentWaveIndex];

            SoulsAscendedCount = 0;
            SoulsLostCount = 0;

            IsWaveComplete = false;
            SpawnController.Instance.LoadNewWave(currentWave);
        }

        #endregion

        #region Score Tracking

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
            if (currentWave == null)
            {
                return 1;
            }

            int soulsNotLost = currentWave.soulsToSpawn - SoulsLostCount;
            float soulsPerc = (float)soulsNotLost / (float)currentWave.soulsToSpawn;
            return soulsPerc;
        }

        public void GetWinLossThisWave()
        {
            if (currentWave == null)
            {
                return;
            }

            float soulsAscendedPercentage = GetSoulsAscendedPercentage();

            string successMsg = "SUCCESS: ";
            if (soulsAscendedPercentage < currentWave.ascensionRatioToWin)
            {
                successMsg = "FAILED: ";
            }
            Debug.Log("Wave " + CurrentWaveIndex + " completed. " + successMsg + (soulsAscendedPercentage * 100).ToString() + "%");
        }

        #endregion

        public void WaveComplete()
        {
            IsWaveComplete = true;
            //StartCoroutine(WaitForTargetsToDisappearCoroutine());
        }

        private void Win()
        {
            Debug.Log("Won! Resetting...");
            CurrentWaveIndex = -1;
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
            GetWinLossThisWave();
            GetNextWave();
        }



        private IEnumerator WaveBreakCoroutine()
        {
            References.mainCanvasBehaviour.UpdateScoreCounter();
            References.mainCanvasBehaviour.WaveUpdater.StartNewWaveAnimation(CurrentWaveIndex);

            yield return new WaitForSeconds(secsBetweenWaves);
            StartNewWave();
        }

        [ContextMenu("Complete This Wave")]
        public void ForceWaveComplete()
        {
            currentWave = listOfWaves[CurrentWaveIndex];
            SpawnController.Instance.WaveComplete();
        }


    }
}
