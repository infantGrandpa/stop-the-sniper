using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    [RequireComponent(typeof(HealthSystem))]
    public class TargetBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent OnInvalid;

        private HealthSystem healthSystem;

        private void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
        }

        private void OnDisable()
        {
            MarkTargetInvalid();
        }

        public void MarkTargetValid()
        {
            if (IsTargetValid())        //Return if already validated
            {
                return;
            }

            healthSystem.isInvulnerable = false;
            LevelManager.Instance.targets.Add(this);
        }

        public void MarkTargetInvalid()
        {
            if (!IsTargetValid())        //Return if already invalidated
            {
                return;
            }

            healthSystem.isInvulnerable = true;
            OnInvalid.Invoke();
            LevelManager.Instance.targets.Remove(this);
        }

        public bool IsTargetValid()
        {
            if (LevelManager.Instance == null)
            {
                return false;
            }

            if (LevelManager.Instance.targets.Contains(this))
            {
                return true;
            }

            return false;
        }

        public void IncreaseLostSoulCountOnDeath()
        {
            WaveManager.Instance.IncreaseLostSouls();
        }
    }
}
