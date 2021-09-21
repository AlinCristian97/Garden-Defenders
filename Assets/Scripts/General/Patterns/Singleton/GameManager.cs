using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using General.Patterns.Observer;
using General.Patterns.Singleton.Interfaces;
using General.Patterns.State.FSM;
using General.Patterns.State.GameManagerFSM;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General.Patterns.Singleton
{
    public class GameManager : MonoBehaviour, IObservable
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

        [field:SerializeField] public int CurrentLevel { get; private set; }
        [field:SerializeField] public int NumberOfDefenderCardsAllowed { get; private set; }

        [field:Header("Passive Energy Resource")]
        [SerializeField] private EnergyResource _passiveEnergyResourcePrefab;
        [SerializeField] private Transform _passiveEnergyResourceContainer;
        [SerializeField] private float _passiveEnergyResourceCooldownInSeconds;
        [SerializeField] private Sound _passiveEnergyResourceSpawnSFX;
        private IEnumerator _spawningPassiveEnergyResource;

        [field:Header("Choose Defenders State")]
        [field:SerializeField] public List<Defender> AvailableDefendersList { get; private set; }
        
        public List<Defender> ChosenDefendersList { get; } = new List<Defender>();

        public bool LevelDefendersConfirmed { get; private set; }
        
        [field:Header("Get Ready State")]
        [field:SerializeField] public float GetReadyTimeInSeconds { get; private set; } = 5f;

        [field: Header("Lose State")] 
        [field:SerializeField] public Collider2D LoseCollider { get; private set; }


        #region Unity Callbacks

        private void Awake()
        {
            StateMachine = new StateMachine();
            States = new GameManagerStates();
            
            _spawningPassiveEnergyResource = PassiveEnergyResourceCoroutine();

            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
        }

        private void Start()
        {
            StateMachine.Initialize(States.ChooseDefendersState);
        }

        private void Update()
        {
            StateMachine.CurrentState.Execute();

            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     StateMachine.ChangeState(States.WinState);
            // }
        }

        #endregion

        public void StartSpawningPassiveEnergyResource()
        {
            StartCoroutine(_spawningPassiveEnergyResource);
        }

        public void StopSpawningPassiveEnergyResource()
        {
            StopCoroutine(_spawningPassiveEnergyResource);
        }

        private IEnumerator PassiveEnergyResourceCoroutine()
        {
            while (true)
            {
                int randomDecider = Random.Range(1, 3);
                Transform passiveEnergyContainerTransform = _passiveEnergyResourceContainer.transform;

                if (randomDecider == 1)
                {
                    passiveEnergyContainerTransform.position = 
                        new Vector3(
                            -0.2f,
                            passiveEnergyContainerTransform.position.y,
                            0f);
                }
                else
                {
                    passiveEnergyContainerTransform.transform.position =
                        new Vector3(
                            -3.8f, 
                            _passiveEnergyResourceContainer.transform.position.y,
                            0f);
                }
                
                EnergyResource energy = Instantiate(_passiveEnergyResourcePrefab, _passiveEnergyResourceContainer);
                AudioManager.Instance.PlayClipAtPoint(_passiveEnergyResourceSpawnSFX, energy.transform.position);
                
                yield return new WaitForSeconds(_passiveEnergyResourceCooldownInSeconds);
            }
        }
        
        public void ConfirmDefendersLevel()
        {
            LevelDefendersConfirmed = true;
        }

        public void AddChosenDefender(Defender defender)
        {
            ChosenDefendersList.Add(defender);
            NotifyObservers();
        }

        public void RemoveChosenDefender(Defender defender)
        {
            ChosenDefendersList.Remove(defender);
            NotifyObservers();
        }

        public IEnumerator DeactivateAllActiveDefenders()
        {
            Defender[] activeDefenders = GetComponentsInChildren<Defender>();
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