using System;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using General.Patterns.State.FSM;
using General.Patterns.State.GameManagerFSM;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        private static GameManager _instance;
        
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
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
        
        #region FSM

        public StateMachine StateMachine { get; private set; }
        public GameManagerStates States { get; private set; }
        
        #endregion


        public bool LevelDefendersConfirmed { get; private set; }
        
        [field:SerializeField] public List<Defender> AvailableDefendersList { get; private set; }
        public List<Defender> ChosenDefendersList { get; private set; } = new List<Defender>();


        // private IPauseManager _pauseManager;

        private void Awake()
        {
            StateMachine = new StateMachine();
            States = new GameManagerStates();
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.MainCanvas, false);
        }

        private void Start()
        {
            StateMachine.Initialize(States.ChooseDefendersState); }

        private void Update()
        {
            StateMachine.CurrentState.Execute();
        }

        public void ConfirmDefendersLevel()
        {
            LevelDefendersConfirmed = true;
        }

        public void UpdateChosenDefendersList(List<Defender> chosenDefendersList)
        {
            ChosenDefendersList = chosenDefendersList;
        }
    }
}