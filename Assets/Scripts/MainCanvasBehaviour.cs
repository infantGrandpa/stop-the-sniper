using TMPro;
using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class MainCanvasBehaviour : MonoBehaviour
    {
        [SerializeField] TMP_Text ascendedText;
        [SerializeField] TMP_Text lostText;

        [SerializeField] TMP_Text waveText;


        public Canvas MainCanvas { get;  private set; }

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

        public void UpdateWave(int currentWave)
        {
            waveText.text = "Wave " + currentWave;

        }
    }
}
