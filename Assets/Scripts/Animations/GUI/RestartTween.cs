using UnityEngine;
using DG.Tweening;

namespace SniperProject
{
    public class RestartTween : MonoBehaviour
    {
        [SerializeField] string tweenID;

        public void RestartMyTween()
        {
            DOTween.Restart(tweenID);
            DOTween.Play(tweenID);
        }
    }
}
