using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{

    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float secsBetweenShots;
        [SerializeField] Transform bulletSpawnTransform;

        private HomingSignal myHomingSignal;


        private void Start()
        {
            myHomingSignal = GetComponent<HomingSignal>();

            //StartCoroutine(FireWeaponCoroutine());
        }

        private void FireBullet()
        {
            GameObject newBullet = LevelManager.Instance.InstantiateObjectOnDyanmicTransform(bulletPrefab);
            if (!newBullet.TryGetComponent(out BulletBehaviour bulletBehaviour))
            {
                Debug.LogError("ERROR WeaponBehaviour FireBullet(): Bullet Prefab (" + bulletPrefab.name + " does not have an attached BulletBehaviour component.", this);
                return;
            }

            Transform bulletTransform = newBullet.transform;
            bulletTransform.position = bulletSpawnTransform.position;

            bulletTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            bulletBehaviour.InitBullet(myHomingSignal.CurrentTarget);   
        }

        private IEnumerator FireWeaponCoroutine()
        {
            yield return new WaitForSeconds(secsBetweenShots);
            FireBullet();

            StartCoroutine(FireWeaponCoroutine());
        }
    }

}