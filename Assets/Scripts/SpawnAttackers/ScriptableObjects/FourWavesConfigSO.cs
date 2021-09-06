using System;
using UnityEngine;

namespace SpawnAttackers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Four Waves Config", menuName = "Waves Configs/Four Waves Config", order = 1)]
    public class FourWavesConfigSO : WavesConfigSO
    {
        private const int WAVES_COUNT = 4;

        private void OnValidate()
        {
            if (LevelWaves.Length != WAVES_COUNT)
            {
                Debug.LogWarning("Don't change the 'LevelWaves' field's array size!");
                Array.Resize(ref LevelWaves, WAVES_COUNT);
            }
        }
    }
}
