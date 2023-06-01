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

        public WaveTextUpdater WaveUpdater { get; private set; }
        public Canvas MainCanvas { get;  private set; }

        public static MainCanvasBehaviour Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(MainCanvasBehaviour)) as MainCanvasBehaviour;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static MainCanvasBehaviour _instance;


        private void Awake()
        {
            MainCanvas = GetComponent<Canvas>();
            WaveUpdater = GetComponent<WaveTextUpdater>();
        }

        private void Start()
        {
            UpdateScoreCounter();
        }

        public Vector3 ConvertWorldToCanvas(Vector3 targetPosition)
        {
            Vector3 screenPosition = LevelManager.Instance.MainCamera.WorldToScreenPoint(targetPosition);
            return screenPosition;
        }

        public Vector3 ConvertCanvasToWorld(Vector3 targetPosition)
        {
            Vector3 worldPosition = LevelManager.Instance.MainCamera.ScreenToWorldPoint(targetPosition);
            return worldPosition;
        }

        public void UpdateScoreCounter()
        {
            ascendedText.text = WaveManager.Instance.SoulsAscendedCount.ToString();
            lostText.text = WaveManager.Instance.SoulsLostCount.ToString();
        }
    }
}
