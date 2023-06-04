using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace SniperProject
{
    public class WaveTextUpdater : MonoBehaviour
    {
        [SerializeField] TMP_Text waveText;
        [SerializeField] UnityEvent onNewWaveEvent;
        [SerializeField] string waveLabel = "Wave";
        private int nextWaveIndex;

        [SerializeField] float secsBeforeWaveChangeEvent;
        [SerializeField] UnityEvent onWaveChangedEvent;

        public string GetWaveString(int newWaveIndex)
        {
            string newTextString = waveLabel + " " + newWaveIndex;
            newTextString = newTextString.Trim();
            return newTextString;
        }

        public void ChangeWaveTextString()
        {
            string newWaveString = GetWaveString(nextWaveIndex + 1);

            waveText.text = newWaveString;
            StartCoroutine(OnWaveChangedEventCoroutine());
        }

        public void StartNewWaveAnimation(int newWaveIndex)
        {
            nextWaveIndex = newWaveIndex;
            onNewWaveEvent?.Invoke();
        }

        [ContextMenu("Test Wave Animation")]
        public void TestWaveAnimation()
        {
            nextWaveIndex++;
            StartNewWaveAnimation(nextWaveIndex);
        }

        private IEnumerator OnWaveChangedEventCoroutine()
        {
            yield return new WaitForSeconds(secsBeforeWaveChangeEvent);

            onWaveChangedEvent?.Invoke();
        }
    }
}
