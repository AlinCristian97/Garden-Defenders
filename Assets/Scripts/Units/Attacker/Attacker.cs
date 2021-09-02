using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : Unit
{
    [Header("Stats")]
    [field:SerializeField] [Range(0.25f, 2f)] private float _movementSpeed = 1f;
    public float MovementSpeed => _movementSpeed;

    protected override float AttackRange => 0.2f;

    public AttackerStates States { get; private set; }

    #region Debug

    private Color _debugColor = Color.gray;

    #endregion

    
    
    #region Unity Callbacks
    
    protected override void Awake()
    {
        base.Awake();
        
        States = new AttackerStates(this);
    }
    
    private void Start()
    {
        StateMachine.Initialize(States.WalkState);
    }

    protected override void Update()
    {
        base.Update();
        
        //TODO: Delete after debugging
        HandleDebug();
    }

    #endregion

    #region Animation Event Methods

    private void Hit()
    {
        Debug.Log(name + "just attacked!");
    }

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    #endregion


    #region Debug Methods

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = AttackRange;
        Vector2 direction = FacingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private void HandleDebug()
    {
        if (!HasTargetInAttackRange())
        {
            _debugColor = Color.red;
        }
        else
        {
            _debugColor = Color.green;
        }
    }

    #endregion
}