using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class ParticleController : MonoBehaviour
    {
        public List<ParticleSystem> particleSystems = new();

        public bool IsActive()
        {
            foreach (ParticleSystem thisParticleSystem in particleSystems)
            {
                if (thisParticleSystem.isPlaying)
                {
                    return true;
                }
            }

            return false;
        }

        private void StopAllEmittors()
        {
            foreach (ParticleSystem thisParticleSystem in particleSystems)
            {
                thisParticleSystem.Stop();
            }
        }

        public void StartDestroyCoroutine()
        {
            StartCoroutine(DestroyGameObjectOnInactive());
        }

        private IEnumerator DestroyGameObjectOnInactive()
        {
            StopAllEmittors();

            while (IsActive())
            {
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
