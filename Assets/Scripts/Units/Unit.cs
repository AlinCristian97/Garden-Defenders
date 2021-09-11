using System;
using System.Collections;
using General.Patterns.FSM;
using HealthSystem.Interfaces;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    #region Components

    public Collider2D Collider { get; protected set; }
    public Animator Animator { get; private set; }

    #endregion

    #region FSM

    public StateMachine StateMachine { get; private set; }

    #endregion

    public int CurrentHealth { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    
    public bool IsDead => CurrentHealth <= 0;
    private bool _isDying;

    #region Unity Callbacks

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        
        StateMachine = new StateMachine();
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
                StartDying();
                _isDying = true;
            }
        }
    }

    private void StartDying()
    {
        StartCoroutine(ProcessDeath());
    }

    protected abstract IEnumerator ProcessDeath();

    protected abstract void SetDeadState();
}