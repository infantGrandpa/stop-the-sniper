using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHealth;
        public float CurrentHealth { get; private set; }

        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        public void Damage(int damageTaken)
        {
            CurrentHealth -= damageTaken;

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
