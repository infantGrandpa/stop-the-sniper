using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHealth;
        [SerializeField] bool destroyOn0Health = true;
        public float CurrentHealth { get; private set; }

        [SerializeField] UnityEvent onDeathEvent;


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
            onDeathEvent?.Invoke();

            if (destroyOn0Health)
            {
                Destroy(gameObject);
            }
        }
    }
}
