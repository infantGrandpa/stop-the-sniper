using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SniperProject
{
    public class TargetBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent OnInvalid;


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

            LevelManager.Instance.targets.Add(this);
        }

        public void MarkTargetInvalid()
        {
            if (!IsTargetValid())        //Return if already invalidated
            {
                return;
            }

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
