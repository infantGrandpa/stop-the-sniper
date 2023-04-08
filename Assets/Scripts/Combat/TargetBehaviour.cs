using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class TargetBehaviour : MonoBehaviour
    {
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
    }
}
