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

        private AnimationController animationController;
        [SerializeField] UnityEvent onFireEvent;

        private Coroutine fireCoroutine;

        private void Start()
        {
            animationController = GetComponent<AnimationController>();
            StartFiring();
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
                DebugHelper.LogError("Bullet Prefab (" + bulletPrefab.name + " does not have an attached BulletBehaviour component.");
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

            fireCoroutine = StartCoroutine(FireWeaponCoroutine());
        }

        public void StopFiring()
        {
            StopCoroutine(fireCoroutine);
        }

        public void StartFiring()
        {
            if (fireCoroutine != null)
            {
                return;
            }

            fireCoroutine = StartCoroutine(FireWeaponCoroutine());
        }

    }

}