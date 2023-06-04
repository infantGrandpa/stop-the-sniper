using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(LevelManager)) as LevelManager;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static LevelManager instance;

        public List<TargetBehaviour> targets = new();

        public Transform DynamicTransform { get; private set; }
        public TargetReticleController TargetReticleController { get; private set; }
        public Camera MainCamera { get; private set; }

        private void Awake()
        {
            DynamicTransform = GameObject.Find("_Dynamic").transform;
            TargetReticleController = GetComponent<TargetReticleController>();
            MainCamera = Camera.main;

        }

        public GameObject InstantiateObjectOnDyanmicTransform(GameObject original)
        {
            GameObject instance = Instantiate(original);
            instance.transform.parent = DynamicTransform;
            return instance;
        }
    }
}