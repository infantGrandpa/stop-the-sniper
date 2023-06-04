using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
namespace SniperProject
{
    public class RandomizeDuration : MonoBehaviour
    {
        private DOTweenAnimation animator;

        [MinMaxSlider(0, 60, true)]
        public Vector2 range = new Vector2(0, 60);

        private void Awake()
        {
            animator = GetComponent<DOTweenAnimation>();
            Randomize();
        }

        public void Randomize()
        {
            animator.duration = Mathf.RoundToInt(Random.Range(range.x, range.y));
        }
    }
}
