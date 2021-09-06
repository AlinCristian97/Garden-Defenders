using System;
using System.Collections;
using System.Collections.Generic;
using SpawnAttackers.ScriptableObjects;
using UnityEngine;

namespace SpawnAttackers
{
    public class AttackerSpawner : MonoBehaviour
    {
        [SerializeField] private WavesConfigSO _spawnConfig;
        
        [SerializeField] private float _timeBetweenSpawns = 10f;
        private float _nextSpawn;

        
    }
}