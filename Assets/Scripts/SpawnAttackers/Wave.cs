using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnAttackers
{
    [Serializable]
    public class Wave
    {
        [field:SerializeField] public Attacker[] AttackersToSpawn { get; set; }
        
        public IEnumerator GetEnumerator()
        {
            return new WavesEnumerator(AttackersToSpawn);
        }

        public int Length => AttackersToSpawn.Length;
    }
    
    public class WavesEnumerator : IEnumerator
    {
        private readonly Attacker[] _attackers;
        private int _position = -1;

        public WavesEnumerator(Attacker[] list)
        {
            _attackers = list;
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _attackers.Length);
        }

        public void Reset() => _position = -1;

        object IEnumerator.Current => Current;

        private Attacker Current
        {
            get
            {
                try
                {
                    return _attackers[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}