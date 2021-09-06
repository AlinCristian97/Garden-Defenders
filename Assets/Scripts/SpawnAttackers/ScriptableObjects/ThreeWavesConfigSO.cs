using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnAttackers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Three Waves Config", menuName = "Waves Configs/Three Waves Config", order = 0)]
    public class ThreeWavesConfigSO : WavesConfigSO
    {
        private const int WAVES_COUNT = 3;

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
