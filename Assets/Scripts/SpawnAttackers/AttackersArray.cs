using System;
using System.Collections;
using UnityEngine;

namespace SpawnAttackers
{
    [Serializable]
    public class AttackersArray : IEnumerable
    {
        [SerializeField] private Attacker[] _attackers;

        public IEnumerator GetEnumerator()
        {
            return new AttackersEnumerator(_attackers);
        }
    }
    
    public class AttackersEnumerator : IEnumerator
    {
        private readonly Attacker[] _attackers;
        private int _position = -1;

        public AttackersEnumerator(Attacker[] list)
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
