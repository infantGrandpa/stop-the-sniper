using UnityEngine;

namespace SniperProject
{
    public class ResetTimeScale : MonoBehaviour
    {
        [ContextMenu("Reset Time Scale")]
        private void Start()
        {
            Time.timeScale = 1;
        }
    }
}
