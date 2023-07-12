using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class FadeToBlack : MonoBehaviour
    {
        [SerializeField] GameObject fadeToBlackObject;
        public void StartFadeToBlack()
        {
            fadeToBlackObject.SetActive(true);
        }
    }
}
