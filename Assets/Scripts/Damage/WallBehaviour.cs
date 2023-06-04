using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace SniperProject
{
    public class WallBehaviour : MonoBehaviour
    {

        #region Properties and Variables
        [Header("References")]
        [SerializeField, RequiredListLength(1, null)] 
        List<GameObject> wallObjects;
        private Object[,] wallObjectData;

        [Header("Spawning")]
        [MinMaxSlider(-360, 360, true)]
        [SerializeField] Vector2 minMaxRotation = new(-360, 360);
        [SerializeField] UnityEvent onInitializeAsWallEvent;

        [Header("As Ghost...")]
        [SerializeField] Material validPostionMaterial;
        [SerializeField] Material invalidPostionMaterial;
        private Material[] startMaterial;

        private bool isGhost;

        #endregion

        #region Initialization

        private void Awake()
        {
            if (wallObjects.Count == 0)
            {
                DebugHelper.LogError("No wall objects assigned.");
            }

            GetStartMaterials();
        }

        private void Start()
        {
            //If the wall starts with the scene, we need to initialize it
            if (isGhost)
            {
                return;
            }

            InitializeAsWall();
        }

        private void GetStartMaterials()
        {
            int rowCount = wallObjects.Count;
            int columnCount = 3;

            wallObjectData = new Object[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                GameObject thisGameObject = wallObjects[i];
                wallObjectData[i, 0] = thisGameObject;

                if (!thisGameObject.TryGetComponent(out MeshRenderer thisMeshRenderer))
                {
                    DebugHelper.LogError(thisGameObject.name + " is missing a MeshRenderer component");
                    continue;
                }

                wallObjectData[i, 1] = thisMeshRenderer;
                wallObjectData[i, 2] = thisMeshRenderer.material;
            }
        }

        public void InitializeAsGhost()
        {
            SetLayerAllChildren(References.nonCollidingObjectsLayerInt);
            ChooseRandomRotation();
            isGhost = true;
        }

        public void InitializeAsWall()
        {
            ResetMaterial();
            SetLayerAllChildren(References.obstacleLayerInt);
            onInitializeAsWallEvent?.Invoke();

            isGhost = false;
        }

        private void ChooseRandomRotation()
        {
            float randomAngle = Random.Range(minMaxRotation.x, minMaxRotation.y);
            Vector3 rotation = new(transform.eulerAngles.x, transform.eulerAngles.y, randomAngle);
            transform.eulerAngles = rotation;
        }

        private void SetLayerAllChildren(int layer)
        {
            var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = layer;
            }
        }

        #endregion

        #region Ghost Handling

        private void ResetMaterial()
        {
            for(int i = 0; i < wallObjectData.GetLength(0); i++)
            {
                GameObject thisGameObject = (GameObject)wallObjectData[i, 0];
                if (!thisGameObject.activeInHierarchy)
                {
                    continue;
                }

                MeshRenderer renderer = (MeshRenderer)wallObjectData[i, 1];
                Material startMaterial = (Material)wallObjectData[i, 2];

                renderer.material = startMaterial;
            }
        }

        private void SetMaterial(Material newMaterial)
        {
            for (int i = 0; i < wallObjectData.GetLength(0); i++)
            {
                GameObject thisGameObject = (GameObject)wallObjectData[i, 0];
                if (!thisGameObject.activeInHierarchy)
                {
                    continue;
                }

                MeshRenderer renderer = (MeshRenderer)wallObjectData[i, 1];

                renderer.material = newMaterial;
            }
        }

        public void UpdateGhostPosition(Vector2 newPosition)
        {
            if (!isGhost)
            {
                return;
            }

            transform.position = newPosition;
            SetGhostMaterial();
        }

        public bool IsCurrentPositionValid()
        {
            bool isValid = true;

            for (int i = 0; i < wallObjectData.GetLength(0); i++)
            {
                GameObject thisGameObject = (GameObject)wallObjectData[i, 0];
                if (!thisGameObject.activeInHierarchy)
                {
                    continue;
                }

                if (!thisGameObject.TryGetComponent(out Collider2D thisCollider))
                {
                    DebugHelper.LogError(thisGameObject.name + " is missing a Collider2D component");
                    continue;
                }
                bool isColliding = thisCollider.IsTouchingLayers(References.obstacleLayerMaskInt);
                if (isColliding)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        private void SetGhostMaterial()
        {
            Material targetMaterial = invalidPostionMaterial;
            if (IsCurrentPositionValid())
            {
                targetMaterial = validPostionMaterial;
            }

            SetMaterial(targetMaterial);
        }

        #endregion

        #region On Destroy

        public void RescanNavMesh()
        {
            DisableObjects();
            AstarPath.active.Scan();
        }

        private void DisableObjects()
        {
            for (int i = 0; i < wallObjectData.GetLength(0); i++)
            {
                GameObject thisGameObject = (GameObject)wallObjectData[i, 0];
                thisGameObject.SetActive(false);
            }
        }
        
        #endregion
    }
}
