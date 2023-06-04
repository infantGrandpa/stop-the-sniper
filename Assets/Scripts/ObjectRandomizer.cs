using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class ObjectRandomizer : MonoBehaviour
    {
        [SerializeField, RequiredListLength(1, null)] 
        List<GameObject> gameObjects = new();

        private void Awake()
        {
            SelectRandomObject();
        }

        private void SelectRandomObject()
        {
            if (gameObjects.Count == 0)
            {
                DebugHelper.LogError("Game objects list is empty.");
                return;
            }

            //Select Object
            int randomIndex = Random.Range(0, gameObjects.Count);

            //Disable all expect our selected object
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject thisObject = gameObjects[i];
                if (i == randomIndex)
                {
                    thisObject.SetActive(true);
                    continue;
                }

                thisObject.SetActive(false);
            }
        }
    }
}
