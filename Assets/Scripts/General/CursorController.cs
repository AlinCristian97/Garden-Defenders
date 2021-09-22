using System;
using UnityEngine;

namespace General
{
    public class CursorController : MonoBehaviour
    {
        #region Singleton

        public static CursorController Instance;

        #endregion

        public Texture2D _cursor;
        
        private void Awake()
        {
            #region Singleton

            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            #endregion

            Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            
        }
    }
}
