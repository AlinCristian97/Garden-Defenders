using System;
using General.FSM;
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
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " has died.");
        Destroy(gameObject);
    }
}