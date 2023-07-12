using UnityEngine;

namespace SniperProject
{
    public class DisableObjectsOnStart : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToDisable;

        private void Start()
        {
            DisableObjects();
        }

        private void DisableObjects()
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}
