using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SniperProject
{
    public class ParticleController : MonoBehaviour
    {
        public List<ParticleSystem> particleSystems = new();

        private float maxParticleLife;
        private bool isLooping;     //If no effects are looping, we can start destroying the particle system immediately

        [SerializeField] float secsToDestroyAnyway = 5f;

        private void Start()
        {
            GetParticleLoop();
            GetMaxParticleLifetime();

            if (!isLooping)
            {
                StartDestroyCoroutineAfterDelay(maxParticleLife);
            }

            StartCoroutine(DestroyAnywayCoroutine());
        }

        private void GetParticleLoop()
        {
            isLooping = particleSystems.Any(ps => ps.main.loop);
        }

        private void GetMaxParticleLifetime()
        {
            maxParticleLife = particleSystems.Max(ps => ps.main.startLifetime.constantMax);
        }

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
                var emission = thisParticleSystem.emission;
                emission.enabled = false;
            }
        }

        public void StartDestroyCoroutine()
        {
            StartCoroutine(DestroyGameObjectOnInactive());
        }

        private IEnumerator DestroyGameObjectOnInactive()
        {
            StopAllEmittors();
            yield return new WaitForSeconds(maxParticleLife);
            Destroy(gameObject);
        }

        public void StartDestroyCoroutineAfterDelay(float secsToDelay = 0f)
        {
            if (secsToDelay <= 0f)
            {
                if (maxParticleLife == 0)
                {
                    GetMaxParticleLifetime();
                }
                secsToDelay = maxParticleLife;
            }

            DestroyAfterDelayCoroutine(secsToDelay);
        }

        private IEnumerator DestroyAfterDelayCoroutine(float secsToDelay)
        {
            yield return new WaitForSeconds(secsToDelay);
            StartDestroyCoroutine();
        }

        public void DisableAllEmittorObjects()
        {
            foreach (ParticleSystem thisParticleSystem in particleSystems)
            {
                GameObject thisGameObject = thisParticleSystem.gameObject;
                thisGameObject.SetActive(false);
            }
        }

        public void EnableAllEmittorObjects()
        {
            foreach (ParticleSystem thisParticleSystem in particleSystems)
            {
                GameObject thisGameObject = thisParticleSystem.gameObject;
                thisGameObject.SetActive(true);
            }
        }

        private IEnumerator DestroyAnywayCoroutine()
        {
            //Hacky way to make sure that particles get destroyed and we don't start accumulating them forever and ever and ever and ever
            yield return new WaitForSeconds(secsToDestroyAnyway);

            Destroy(gameObject);
        }
    }
}
