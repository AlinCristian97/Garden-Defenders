using System;
using System.Collections;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using General.Patterns.State.FSM;
using General.Patterns.State.GameManagerFSM;
using UI;
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

        [field:Header("Choose Defenders State")]
        [field:SerializeField] public List<Defender> AvailableDefendersList { get; private set; }
        public List<Defender> ChosenDefendersList { get; private set; } = new List<Defender>();
        public bool LevelDefendersConfirmed { get; private set; }
        
        [field:Header("Get Ready State")]
        [field:SerializeField] public float GetReadyTimeInSeconds { get; private set; }= 5f;

        [field: Header("Lose State")] 
        [field:SerializeField] public Collider2D LoseCollider { get; private set; }


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
        
        public IEnumerator DeactivateAllActiveDefenders()
        {
            Defender[] activeDefenders = Instance.GetComponentsInChildren<Defender>();
            float delayInSeconds = 5f;

            if (activeDefenders.Length > 0)
            {
                yield return new WaitForSeconds(delayInSeconds);

                foreach (Defender activeDefender in activeDefenders)
                {
                    activeDefender.gameObject.SetActive(false);
                }
            }
        }

        public IEnumerator DeactivateAllLawnMowers()
        {
            LawnMower[] activeLawnMowers = Instance.GetComponentsInChildren<LawnMower>();
            float delayInSeconds = 5f;

            if (activeLawnMowers.Length > 0)
            {
                yield return new WaitForSeconds(delayInSeconds);

                foreach (LawnMower activeLawnMower in activeLawnMowers)
                {
                    activeLawnMower.gameObject.SetActive(false);
                }
            }
        }
    }
}