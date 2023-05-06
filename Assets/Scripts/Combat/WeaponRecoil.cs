using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class WeaponRecoil : MonoBehaviour
    {
        private CinemachineImpulseSource recoilSource;

        private void Awake()
        {
            recoilSource = GetComponent<CinemachineImpulseSource>();
        }

        public void GenerateRecoilInDirection(Vector3 targetPosition)
        {
            Vector3 dir = (transform.position - targetPosition).normalized;
            Vector3 newVelocity = dir * recoilSource.m_DefaultVelocity.magnitude;
            recoilSource.m_DefaultVelocity = newVelocity;
        }
    }
}
