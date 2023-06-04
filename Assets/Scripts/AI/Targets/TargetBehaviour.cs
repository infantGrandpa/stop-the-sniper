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
        
        public bool IsValidTarget { get; private set; }

        private void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
        }

        private void OnEnable()
        {
            LevelManager.Instance.targets.Add(this);
        }

        private void OnDisable()
        {
            if (LevelManager.Instance == null)
            {
                return;
            }

            LevelManager.Instance.targets.Remove(this);
        }

        public void MarkTargetValid()
        {
            IsValidTarget = true;
            healthSystem.isInvulnerable = false;   
        }

        public void MarkTargetInvalid()
        {
            IsValidTarget = false;
            healthSystem.isInvulnerable = true;
            OnInvalid?.Invoke();
        }

        public void IncreaseLostSoulCountOnDeath()
        {
            WaveManager.Instance.IncreaseLostSouls();
        }
    }
}
