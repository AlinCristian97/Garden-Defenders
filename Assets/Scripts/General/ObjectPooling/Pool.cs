using UnityEngine;

namespace General.ObjectPooling
{
    [System.Serializable]
    public class Pool
    {
        [field:SerializeField] public string Identifier { get; private set; }
        [field:SerializeField] public GameObject Prefab { get; private set; }
        [field:SerializeField] public int Size { get; private set; }
    }
}