using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    public class EnableOnSpawn : MonoBehaviour, IOnSpawn
    {
        public void OnSpawn(GameObject spawnedObject)
        {
            gameObject.SetActive(true);
        }
    }
}
