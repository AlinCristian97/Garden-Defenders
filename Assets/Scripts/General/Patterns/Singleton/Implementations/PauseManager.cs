using System;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

namespace General.Patterns.Singleton.Implementations
{
    public class PauseManager : MonoBehaviour, IPauseManager
    {
        #region Singleton

        private static PauseManager _instance;
        
        public static PauseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PauseManager>();
                }
                
                return _instance;
            }
        }

        #endregion
        
        #region Observer

        public List<IObserver> Observers { get; private set; } = new List<IObserver>();

        public void AttachObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            if (Observers.Count > 0)
            {
                foreach (IObserver observer in Observers)
                {
                    observer.GetNotified();
                }
            }
        }

        #endregion
        
        [SerializeField] private GameObject _panelCanvas;
        
        public bool GameIsPaused { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            _panelCanvas.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            _panelCanvas.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1;
        }
    }
}