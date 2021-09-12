using System.Collections.Generic;
using UnityEngine;

namespace SpawnAttackers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Waves Config", menuName = "Waves Config", order = 0)]
    public class WavesConfigSO : ScriptableObject
    {
        public Wave[] Waves;
    }
}
