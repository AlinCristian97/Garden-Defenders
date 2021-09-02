using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using UnityEngine;

public class Defender : Unit
{
    //TODO: Does this variable make sense here?
    [field:SerializeField] public Sprite Avatar { get; private set; }
    
    [field:SerializeField] public int Cost { get; private set; }
    public Tile Tile => GetComponentInParent<Tile>();
    
    [Header("Attacking")]
    [SerializeField] private Projectile _projectile;
    
    [Header("Defender Detection")]
    [SerializeField] private LayerMask _detectAttackerLayerMask;
    private const float ATTACK_RANGE = 0.5f;

    #region FSM
    public DefenderStates States { get; private set; }

    #endregion

    //TODO: Unserialize after testing
    [SerializeField] private bool _enemyInLane;

    #region Debug

    private Color _debugColor = Color.gray;

    #endregion
    
    

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        States = new DefenderStates(this);
    }
    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
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
        Debug.Log("Defender - just attacked!");
    }

    private void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    #endregion

    public bool IsNearDefender()
    {        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.right;
        Vector2 endPoint = startPoint + distance * direction;

        
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectAttackerLayerMask);

        return hit.collider != null;
    }

    #region Debug Methods

    private void OnDrawGizmos()
    {
        Collider = GetComponent<Collider2D>();
        
        Vector2 startPoint = GetColliderSideBoundCenterPoint();
        
        float distance = ATTACK_RANGE;
        Vector2 direction = Vector2.right;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private void HandleDebug()
    {
        if (!IsNearDefender())
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
