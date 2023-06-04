using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField] float secsBeforeCanDamage;
        [SerializeField] int damageOnCollision;
        [SerializeField] int damageToTakeOnCollision = 0;

        private HealthSystem myHealthSystem;

        [SerializeField] UnityEvent onDamageEvent;

        private bool canDamage;

        private void Start()
        {
            myHealthSystem = GetComponent<HealthSystem>();

            canDamage = false;
            StartCoroutine(DelayDamageCoroutine());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DamageObject(collision.gameObject);
        }

        private void DamageObject(GameObject objectToDamage)
        {
            if (!canDamage)
            {
                return;
            }

            if (!objectToDamage.TryGetComponent(out IDamageable damageableObject))
            {
                return;
            }

            damageableObject.Damage(damageOnCollision);
            DamageSelf();
            onDamageEvent?.Invoke();
        }

        private void DamageSelf()
        {
            if (myHealthSystem == null)
            {
                return;
            }

            myHealthSystem.Damage(damageToTakeOnCollision);
        }

        private IEnumerator DelayDamageCoroutine()
        {
            yield return new WaitForSeconds(secsBeforeCanDamage);
            canDamage = true;
        }
    }
}
