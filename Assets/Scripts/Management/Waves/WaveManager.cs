using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public int SoulsThisWave { get; private set; }

        private int totalSoulsAscended;
        private int totalSoulsLost;

        public UnityEvent onWinEvent;

        [SerializeField] UnityEvent onWaveCompleteEvent;

        public bool HasWonGame { get; private set; }
        

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
                DebugHelper.LogError("No assigned waves.");
            }
            HasWonGame = false;
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

            //SoulsAscendedCount = 0;
            //SoulsLostCount = 0;

            SoulsThisWave = currentWave.soulsToSpawn;
            MainCanvasBehaviour.Instance.UpdateAllCounters();

            TargetSpawnController.Instance.LoadNewWave(currentWave);
            CanisterSpawnController2.Instance.LoadNewWave(currentWave);
        }

        #endregion

        #region Score Tracking

        public void IncreaseAscendedSouls(int increaseBy = 1)
        {
            SoulsAscendedCount += increaseBy;
            totalSoulsAscended += increaseBy;
            MainCanvasBehaviour.Instance.UpdateAscendedSouls();
        }

        public void IncreaseLostSouls(int increaseBy = 1)
        {
            SoulsLostCount += increaseBy;
            totalSoulsLost += increaseBy;
            MainCanvasBehaviour.Instance.UpdateLostSouls();
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

        #endregion

        public void WaveComplete()
        {
            onWaveCompleteEvent?.Invoke();
        }

        private void Win()
        {
            Debug.Log("Won!");
            onWinEvent?.Invoke();
            CanvasPauseMenu.Instance.ShowWinScreen(totalSoulsAscended, totalSoulsLost);
            HasWonGame = true;
        }

        private IEnumerator WaveBreakCoroutine()
        {
            MainCanvasBehaviour.Instance.UpdateAscendedSouls();
            MainCanvasBehaviour.Instance.WaveUpdater.StartNewWaveAnimation(CurrentWaveIndex);

            yield return new WaitForSeconds(secsBetweenWaves);
            StartNewWave();
        }

        public bool IsWaveComplete()
        {
            return TargetSpawnController.Instance.spawnedThisWave < SoulsThisWave;
        }
    }
}
