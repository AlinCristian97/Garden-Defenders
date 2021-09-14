using UnityEngine;

namespace General.Patterns.Singleton
{
    public abstract class SingletonBase<T> : MonoBehaviour
        where T : class
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                        Debug.LogError("SingletonBase<T>: Could not find GameObject of type " + typeof(T).Name);
                }
                return _instance;
            }
        }
    }
}