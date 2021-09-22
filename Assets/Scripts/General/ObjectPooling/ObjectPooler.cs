using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.ObjectPooling
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Singleton

        private static ObjectPooler _instance;
        
        public static ObjectPooler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ObjectPooler>();
                }
                
                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private List<Pool> _pools;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in _pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.Size; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab, transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                
                _poolDictionary.Add(pool.Identifier, objectPool);
            }
        }

        public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(identifier))
            {
                Debug.LogWarning("Pool with identifier " + identifier + " doesn't exist.");
                return null;
            }
            
            GameObject objectToSpawn = _poolDictionary[identifier].Dequeue();
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            
            _poolDictionary[identifier].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!_poolDictionary.ContainsKey(identifier))
            {
                Debug.LogWarning("Pool with identifier " + identifier + " doesn't exist.");
                return null;
            }
            
            GameObject objectToSpawn = _poolDictionary[identifier].Dequeue();
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.transform.SetParent(parent);
            
            _poolDictionary[identifier].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}