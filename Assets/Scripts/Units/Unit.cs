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

    [field: SerializeField] public int Health { get; private set; }
    public bool IsDead => Health <= 0;
    private bool _isDying;

    #region Unity Callbacks

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        
        StateMachine = new StateMachine();
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Execute();
    }
    
    #endregion

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Health = 0;

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