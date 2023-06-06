using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class SpawnObjectOnCommand : MonoBehaviour
    {
        [SerializeField] List<GameObject> objectsToSpawn = new();

        [SerializeField] bool matchRotation;

        public void SpawnObjects()
        {
            foreach (GameObject thisObject in objectsToSpawn)
            {
                GameObject newObject = LevelManager.Instance.InstantiateObjectOnDyanmicTransform(thisObject);
                newObject.transform.position = transform.position;

                if (matchRotation)
                {
                    newObject.transform.eulerAngles = transform.eulerAngles;
                }
            }
        }
    }
}
