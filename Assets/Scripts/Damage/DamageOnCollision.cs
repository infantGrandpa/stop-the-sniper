using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField] int damageOnCollision;
        [SerializeField] int damageToTakeOnCollision = 0;

        private HealthSystem myHealthSystem;

        private void Start()
        {
            myHealthSystem = GetComponent<HealthSystem>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DamageObject(collision.gameObject);
        }

        private void DamageObject(GameObject objectToDamage)
        {
            if (!objectToDamage.TryGetComponent(out IDamageable damageableObject))
            {
                return;
            }

            damageableObject.Damage(damageOnCollision);
            DamageSelf();
        }

        private void DamageSelf()
        {
            if (myHealthSystem == null)
            {
                return;
            }

            myHealthSystem.Damage(damageToTakeOnCollision);
        }
    }
}
