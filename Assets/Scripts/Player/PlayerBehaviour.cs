using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public static PlayerBehaviour Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static PlayerBehaviour _instance;

        
        [SerializeField] HomingSignal _homingSignal;

        public HomingSignal PlayerHomingSignal
        {
            get { return _homingSignal; }
            private set { _homingSignal = value; }
        }


        [SerializeField] WeaponBehaviour _weaponBehaviour;

        public WeaponBehaviour PlayerWeaponBehaviour
        {
            get { return _weaponBehaviour; }
            private set { _weaponBehaviour = value; }
        }

        public void StopFiring()
        {
            _weaponBehaviour.StopFiring();
        }

        public void StartFiring()
        {
            _weaponBehaviour.StartFiring();
        }
    }
}
