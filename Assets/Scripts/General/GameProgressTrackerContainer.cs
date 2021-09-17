using System;
using UnityEngine;

namespace General
{
    public class GameProgressTrackerContainer : MonoBehaviour
    {
        public static GameProgressTracker LoadedProgress = null;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}