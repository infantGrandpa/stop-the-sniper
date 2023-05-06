using System.Collections.Generic;
using UnityEngine;

namespace SniperProject
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        public int ascensionsToProgress;
        public int lossesToFail;

        public List<GameObject> enemyTypes = new();

        public List<GameObject> soulTypes = new();
    }
}
