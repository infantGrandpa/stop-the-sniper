using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SniperProject
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        [BoxGroup("Soul Spawning"), ValidateInput("ValidateGreaterThan0", "Souls to Spawn must be greater than 0", InfoMessageType.Error)]
        public int soulsToSpawn = 5;
        [BoxGroup("Soul Spawning"), MinMaxSlider(0, 10f), Space]
        public Vector2 secsBetweenSpawns = new(0, 5);

        [BoxGroup("Soul Spawning"), Range(0, 1), Space]
        public float ascensionRatioToWin = 0.6f;
        [BoxGroup("Soul Spawning"), Space]
        public List<GameObject> soulTypes = new();

        [BoxGroup("Enemy Spawning")]
        public List<GameObject> enemyTypes = new();

        private bool ValidateGreaterThan0(int value)
        {
            return value > 0;
        }
    }
}
