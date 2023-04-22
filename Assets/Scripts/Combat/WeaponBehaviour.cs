using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    [RequireComponent(typeof (HomingSignal))]
    public class WeaponBehaviour : MonoBehaviour
    {
        public GameObject bulletPrefab;
        [SerializeField] float secsBetweenShots;
        [SerializeField] Transform bulletSpawnTransform;
        [SerializeField] bool canFire;

        private HomingSignal myHomingSignal;
        private LineRendererColor rendererColor;


        private void Start()
        {
            myHomingSignal = GetComponent<HomingSignal>();
            rendererColor = GetComponent<LineRendererColor>();

            StartCoroutine(FireWeaponCoroutine());
        }

        private void FireBullet()
        {
            if (!canFire)
            {
                return;
            }

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

            if (rendererColor != null)
            {
                rendererColor.ResetLineColor();
            }
        }

        private IEnumerator FireWeaponCoroutine()
        {
            if (rendererColor != null)
            {
                rendererColor.TweenLineColor(secsBetweenShots);
            }
            yield return new WaitForSeconds(secsBetweenShots);
            FireBullet();

            StartCoroutine(FireWeaponCoroutine());
        }
    }

}