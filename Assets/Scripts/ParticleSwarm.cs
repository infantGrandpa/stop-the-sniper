using UnityEngine;

namespace SniperProject
{
    public class ParticleSwarm : MonoBehaviour
    {
        private ParticleSystem myParticleSystem;
        private ParticleSystem.Particle[] particles;

        void Start()
        {
            myParticleSystem = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[myParticleSystem.main.maxParticles];
        }

        void LateUpdate()
        {
            int particleCount = myParticleSystem.GetParticles(particles);

            for (int i = 0; i < particleCount; i++)
            {
                // Get the current particle's velocity
                Vector3 velocity = particles[i].velocity;

                // Update the particle's position based on its velocity
                particles[i].position += velocity * Time.deltaTime;

                // Rotate the particle to face its direction of movement
                particles[i].rotation = Quaternion.LookRotation(velocity).eulerAngles.y;
            }

            // Update the modified particles back to the Particle System
            myParticleSystem.SetParticles(particles, particleCount);
        }
    }
}
