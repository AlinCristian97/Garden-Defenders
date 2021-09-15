using System;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using General.Patterns.State.FSM;
using General.Patterns.State.GameManagerFSM;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class GameManager : MonoBehaviour, IGameManager
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

        // private IPauseManager _pauseManager;

        private void Awake()
        {
            StateMachine = new StateMachine();
            States = new GameManagerStates();
        }

        private void Start()
        {
            StateMachine.Initialize(States.ChooseDefendersState);
        }

        private void Update()
        {
            StateMachine.CurrentState.Execute();
        }
    }
}