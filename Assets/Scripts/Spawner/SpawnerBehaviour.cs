using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class SpawnerBehaviour : MonoBehaviour
    {
        private enum SpawnerType
        {
            Targets,
            Canisters
        }
        [SerializeField, EnumToggleButtons] SpawnerType spawnerType;
        
        private IOnSpawn onSpawnBehaviour;

        public bool IsOccupied { get; private set; }

        private void Awake()
        {
            onSpawnBehaviour = GetComponent<IOnSpawn>();
        }

        private void OnEnable()
        {
            
            switch (spawnerType)
            {
                case SpawnerType.Canisters:
                    CanisterSpawnController2.Instance.spawners.Add(this);
                    break;
                default:
                    TargetSpawnController.Instance.spawners.Add(this);
                    break;
            }
            
            //SpawnController.Instance.spawners.Add(this);

        }

        private void OnDisable()
        {
            switch (spawnerType)
            {
                case SpawnerType.Canisters:
                    if (CanisterSpawnController2.Instance == null) { return; }
                    CanisterSpawnController2.Instance.spawners.Remove(this);
                    break;
                default:
                    if (TargetSpawnController.Instance == null) { return; }
                    TargetSpawnController.Instance.spawners.Remove(this);
                    break;
            }

            //if (SpawnController.Instance == null)
            //{
            //    return;
            //}
            //SpawnController.Instance.spawners.Remove(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            CheckOccupied(collision);
        }

        private void CheckOccupied(Collider2D collision)
        {
            //Ignore the collision of its not the proper type of object
            switch (spawnerType)
            {
                case SpawnerType.Canisters:
                    if (!collision.TryGetComponent(out CanisterBehaviour canister)) { return; }
                    break;
                default:
                    if (!collision.TryGetComponent(out TargetBehaviour target)){ return; }
                    break;
            }


            //if (!collision.TryGetComponent(out TargetBehaviour target))
            //{
            //    return;
            //}
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
            instance.SetActive(true); //Fucking hack but I don't know why canisters are being spawned inactive

            if (onSpawnBehaviour != null)
            {
                onSpawnBehaviour.OnSpawn(instance);
            }

            return instance;
        }

        //Hack but can't find why they won't unoccupy themselves. Should get called for canisters at the end of each wave.
        public void MarkUnoccupied()
        {
            IsOccupied = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
    }
}
