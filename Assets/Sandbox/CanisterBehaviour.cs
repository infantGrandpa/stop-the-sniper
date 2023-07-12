using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class CanisterBehaviour : MonoBehaviour
    {
        [SerializeField] UnityEvent toDestroyEvent;

        public void DestroyCanisterAtEndOfWave()
        {
            if (toDestroyEvent == null)
            {
                DestroyGameObject();
                return;
            }

            toDestroyEvent.Invoke();
        }

        private void OnEnable()
        {
            CanisterSpawnController2.Instance.activeCanisters.Add(this);
        }

        private void OnDisable()
        {
            if (CanisterSpawnController2.Instance == null)
            {
                return;
            }
            CanisterSpawnController2.Instance.activeCanisters.Remove(this);
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}
