using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class RandomizeRotationValues : MonoBehaviour
    {
        private DOTweenAnimation animator;

        [MinMaxSlider(-360, 360, true)]
        public Vector2 range = new Vector2(-360, 360);

        private void Awake()
        {
            animator = GetComponent<DOTweenAnimation>();
            RandomizeRotation();
        }

        public void RandomizeRotation()
        {
            int randomX = (int)Random.Range(range.x, range.y);
            int randomY = (int)Random.Range(range.x, range.y);
            int randomZ = (int)Random.Range(range.x, range.y);

            animator.endValueV3 = new Vector3(randomX, randomY, randomZ);
        }
    }
}
