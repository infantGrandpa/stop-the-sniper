using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class SpawnerBehaviour : MonoBehaviour
    {
        private IOnSpawn onSpawnBehaviour;

        public bool IsOccupied { get; private set; }

        private void Awake()
        {
            onSpawnBehaviour = GetComponent<IOnSpawn>();
        }

        private void OnEnable()
        {
            SpawnController.Instance.spawners.Add(this);
        }

        private void OnDisable()
        {
            if (SpawnController.Instance == null)
            {
                return;
            }
            SpawnController.Instance.spawners.Remove(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out TargetBehaviour target))
            {
                return;
            }

            IsOccupied = false;
        }

        public bool AttemptSpawnObject(GameObject objectToSpawn)
        {
            if (IsOccupied)
            {
                return false;
            }

            SpawnObject(objectToSpawn);
            return true;
        }

        private GameObject SpawnObject(GameObject objectToSpawn)
        {
            GameObject instance = LevelManager.Instance.InstantiateObjectOnDyanmicTransform(objectToSpawn);
            instance.transform.position = transform.position;
            IsOccupied = true;

            if (onSpawnBehaviour != null)
            {
                onSpawnBehaviour.OnSpawn(instance);
            }

            return instance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
    }
}
