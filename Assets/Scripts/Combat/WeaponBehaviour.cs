using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class WeaponBehaviour : MonoBehaviour
    {
        public GameObject bulletPrefab;
        [SerializeField] float secsBetweenShots;
        [SerializeField] Transform bulletSpawnTransform;
        [SerializeField] bool pauseFiringForDebug;

        private WeaponAnimationController animationController;
        [SerializeField] UnityEvent onFireEvent;

        private void Start()
        {
            animationController = GetComponent<WeaponAnimationController>();
            StartCoroutine(FireWeaponCoroutine());
        }

        private void FireBullet()
        {
            if (pauseFiringForDebug)
            {
                return;
            }

            if (PlayerBehaviour.Instance.PlayerHomingSignal == null)
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
            bulletBehaviour.InitBullet(PlayerBehaviour.Instance.PlayerHomingSignal.CurrentTarget);

            onFireEvent?.Invoke();

            if (animationController != null)
            {
                animationController.ResetAnimations();
            }
        }

        private IEnumerator FireWeaponCoroutine()
        {
            if (animationController != null)
            {
                animationController.StartAnimationsAfterReset(secsBetweenShots);
            }

            yield return new WaitForSeconds(secsBetweenShots); ;
            FireBullet();

            StartCoroutine(FireWeaponCoroutine());
        }


    }

}