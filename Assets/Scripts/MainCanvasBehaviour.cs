using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class MainCanvasBehaviour : MonoBehaviour
    {
        [SerializeField] TMP_Text ascendedText;
        [SerializeField] TMP_Text lostText;

        [SerializeField] TMP_Text waveText;

        private bool waveReadyToUpdate = false;

        public Canvas MainCanvas { get;  private set; }

        [SerializeField] UnityEvent onNewWaveEvent;

        private void Awake()
        {
            References.mainCanvasBehaviour = this;
            MainCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            UpdateScoreCounter();
        }

        public Vector3 ConvertWorldToCanvas(Vector3 targetPosition)
        {
            // Convert the world position to a screen position
            Vector3 screenPosition = LevelManager.Instance.MainCamera.WorldToScreenPoint(targetPosition);

            return screenPosition;
        }

        public void UpdateScoreCounter()
        {
            ascendedText.text = WaveManager.Instance.SoulsAscendedCount.ToString();
            lostText.text = WaveManager.Instance.SoulsLostCount.ToString();
        }

        public void SetWaveWithoutAnimation(int currentWave)
        {
            waveText.text = "Wave " + currentWave;
        }

        public void UpdateWave(int currentWave)
        {
            onNewWaveEvent?.Invoke();
            waveReadyToUpdate = false;
            //StartCoroutine(UpdateWaveCoroutine(currentWave));   
        }

        private IEnumerator UpdateWaveCoroutine(int currentWave)
        {
            while (!waveReadyToUpdate)
            {
                yield return null;
            }

            waveText.text = "Wave " + currentWave;
            waveReadyToUpdate = false;
        }


        public void AllowWaveToUpdate()
        {
            waveReadyToUpdate = true;
        }

    }
}
