using System;
using FSM;
using General.Patterns.FSM;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LawnMower : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; } = 1f;

    #region Components

    public Animator Animator { get; private set; }
    public Collider2D Collider { get; private set; }

    #endregion
    
    #region FSM

    public StateMachine StateMachine { get; private set; }
    public LawnMowerStates States { get; private set; }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();

        StateMachine = new StateMachine();
        States = new LawnMowerStates(this);
    }

    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
    }
    
    private void Update()
    {
        StateMachine.CurrentState.Execute();
    }

    #endregion
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (StateMachine.CurrentState == States.ActiveState)
        {
            if (other.GetComponent<Attacker>() != null)
            {
                var attacker = other.GetComponent<Attacker>();
                attacker.StartDying();
                
                Animator.SetTrigger("HitAttacker");
            }
        }
    }
}