using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Collections;

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
        public Camera MainCamera { get; private set; }
        public Plane LevelPlane { get; private set; }

        [SerializeField, ValueDropdown("GetNormals", AppendNextDrawer = true, DisableGUIInAppendedDrawer = true)] 
        Vector3 planeNormal = Vector3.forward;

        [SerializeField] UnityEvent onPause;

        [SerializeField] UnityEvent onUnpause;

        private void Awake()
        {
            CreateDynamicTransform();
            
            MainCamera = Camera.main;

            SetAudioReferences();

            LevelPlane = new Plane(planeNormal, transform.position);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnEscape();
            }
        }

        public GameObject InstantiateObjectOnDyanmicTransform(GameObject original)
        {
            GameObject instance = Instantiate(original);
            instance.transform.parent = DynamicTransform;
            return instance;
        }

        private void CreateDynamicTransform()
        {
            GameObject dynamicGameObject = new() { name = "_Dynamic" };
            DynamicTransform = dynamicGameObject.transform;
        }

        private void SetAudioReferences()
        {
            AudioManager.AudioManager.Instance.defaultParentTransform = DynamicTransform;
            AudioManager.AudioManager.Instance.mainCameraTransform = MainCamera.transform;
        }

        private static IEnumerable GetNormals = new ValueDropdownList<Vector3>()
        {
            { "Up", Vector3.up },
            { "Down", Vector3.down },
            { "Left", Vector3.right },
            { "Right", Vector3.left },
            { "Forward", Vector3.forward },
            { "Back", Vector3.back },
        };

        private void OnDrawGizmosSelected()
        {
            DrawPlaneGizmo();
        }

        private void DrawPlaneGizmo()
        {
            Gizmos.color = Color.blue;

            float planeDistance = 10f;
            float shortDistance = 0f;
            Vector3 cubeSizeVector;
            if (planeNormal == Vector3.up || planeNormal == Vector3.down)
            {
                cubeSizeVector = new Vector3(planeDistance, shortDistance, planeDistance);
            }
            else if (planeNormal == Vector3.right || planeNormal == Vector3.left)
            {
                cubeSizeVector = new Vector3(shortDistance, planeDistance, planeDistance);
            }
            else
            {
                cubeSizeVector = new Vector3(planeDistance, planeDistance, shortDistance);
            }

            Gizmos.DrawWireCube(transform.position, cubeSizeVector);
        }

        public void PauseGame()
        {
            onPause?.Invoke();
            Time.timeScale = 0;
            
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
            onUnpause?.Invoke();
        }

        private void OnEscape()
        {
            if (WaveManager.Instance.HasWonGame)
            {
                return;
            }

            if (Time.timeScale == 0)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}