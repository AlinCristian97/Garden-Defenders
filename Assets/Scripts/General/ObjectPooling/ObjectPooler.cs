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
        private Dictionary<string, List<GameObject>> _poolDictionary;

        private void Start()
        {
            _poolDictionary = new Dictionary<string, List<GameObject>>();

            foreach (Pool pool in _pools)
            {
                List<GameObject> objectPool = new List<GameObject>();

                for (int i = 0; i < pool.Size; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab, transform);
                    obj.SetActive(false);
                    objectPool.Add(obj);
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

            foreach (GameObject obj in _poolDictionary[identifier])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    return obj;
                }
            }

            return null;
        }
        
        public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!_poolDictionary.ContainsKey(identifier))
            {
                Debug.LogWarning("Pool with identifier " + identifier + " doesn't exist.");
                return null;
            }
            
            foreach (GameObject obj in _poolDictionary[identifier])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.transform.SetParent(parent);
                    return obj;
                }
            }

            return null;
        }
    }
}