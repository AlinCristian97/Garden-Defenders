using System;
using General.FSM;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    #region Components

    public Collider2D Collider { get; protected set; }
    public Animator Animator { get; private set; }

    #endregion

    #region FSM

    public StateMachine StateMachine { get; private set; }

    #endregion
    
    
    
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
}