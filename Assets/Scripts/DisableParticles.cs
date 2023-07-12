using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class DisableParticles : MonoBehaviour
    {
        private enum DelayType
        {
            Constant,
            RandomBetweenTwoConstants
        }

        [SerializeField] DelayType delayType;


        [SerializeField, ShowIf("@this.delayType == DelayType.Constant")]
        float secDelay;

        [SerializeField, ShowIf("@this.delayType == DelayType.RandomBetweenTwoConstants")]
        Vector2 secDelayRange;

        [SerializeField] ParticleController particleController;

        [SerializeField] bool reenableAfterDelay;

        private void Start()
        {
            StartCoroutine(DisableEmittorsCoroutine());
        }

        private IEnumerator DisableEmittorsCoroutine()
        {
            yield return new WaitForSeconds(GetDelay());

            particleController.DisableAllEmittorObjects();

            if (reenableAfterDelay)
            {
                StartCoroutine(EnableEmittorsCoroutine());
            }
        }

        private IEnumerator EnableEmittorsCoroutine()
        {
            yield return new WaitForSeconds(GetDelay());

            particleController.DisableAllEmittorObjects();

            DisableEmittorsCoroutine();
        }

        private float GetDelay()
        {
            float delay = 0;
            switch (delayType)
            {
                case DelayType.Constant:
                    delay = secDelay;
                    break;
                case DelayType.RandomBetweenTwoConstants:
                    delay = Random.Range(secDelayRange.x, secDelayRange.y);
                    break;
                default:
                    DebugHelper.LogError("Delay Type not defined.");
                    break;
            }

            return delay;
        }
    }
}
