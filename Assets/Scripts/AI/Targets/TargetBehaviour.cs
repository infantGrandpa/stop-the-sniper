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
            if (LevelManager.Instance == null)
            {
                return;
            }

            if (LevelManager.Instance.targets.Contains(this))
            {
                //Already validated
                return;
            }

            LevelManager.Instance.targets.Add(this);
        }

        public void MarkTargetInvalid()
        {
            if (LevelManager.Instance == null)
            {
                return;
            }

            OnInvalid.Invoke();
            LevelManager.Instance.targets.Remove(this);
        }

        

    }
}
