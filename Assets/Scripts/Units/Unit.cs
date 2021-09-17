using System;
using System.Collections;
using System.Collections.Generic;
using General.Patterns.Observer;
using General.Patterns.State.FSM;
using HealthSystem.Interfaces;
using UI;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IObservable
{
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

    #region Components

    public Collider2D Collider { get; protected set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    #endregion

    #region FSM

    public StateMachine StateMachine { get; private set; }

    #endregion

    private int _currentHealth;
    public int CurrentHealth 
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
            NotifyObservers();
        } 
    }
    [field: SerializeField] public int MaxHealth { get; private set; }
    
    public bool IsDead => CurrentHealth <= 0;
    private bool _isDying;
    
    protected HealthHUD HealthHUD;

    #region Unity Callbacks

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();

        if (GetComponentInChildren<SpriteRenderer>() != null)
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        
        StateMachine = new StateMachine();
        
        HealthHUD = GetComponentInChildren<HealthHUD>();
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Execute();
    }
    
    #endregion

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            if (!_isDying)
            {
                StartCoroutine(ProcessDeath());
                _isDying = true;
            }
        }
    }

    protected abstract IEnumerator ProcessDeath();

    protected abstract void SetDeadState();
}