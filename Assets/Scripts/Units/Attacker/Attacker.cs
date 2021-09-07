using System;
using System.Collections;
using System.Collections.Generic;
using General.FSM;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public class Attacker : Unit
{
    [Header("Stats")]
    [SerializeField] private bool _isFacingRight;
    private Vector2 _facingDirection;
    [field:SerializeField] [Range(0.25f, 2f)] private float _movementSpeed = 1f;
    [SerializeField] private int _damage;
    
    public float MovementSpeed => _movementSpeed;


    [Header("Attacking")]
    private float _attackRange;
    private const float MIN_ATTACK_RANGE = 0.025f;
    private const float MAX_ATTACK_RANGE = 0.4f;
    private const float RANGE_START_OFFSET = 0.3f;
    [SerializeField] private LayerMask _detectTargetLayerMask;
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenAttacks = 1f;
    private float _nextAttack;
    private Defender _target;

    #region FSM

    public AttackerStates States { get; private set; }
    
    private SpriteRenderer _visualsRenderer;
    private SpriteRenderer _shadow;
    private const float FADE_OUT_DURATION = 2f;

    #endregion
    
    #region Debug

    private Color _debugColor = Color.gray;

    #endregion
    
    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        _facingDirection = _isFacingRight ? Vector2.right : Vector2.left;
        States = new AttackerStates(this);
        
        _visualsRenderer = GetComponentInChildren<SpriteRenderer>();
        //TODO: Find better way to get the Shadow
        _shadow = _visualsRenderer.gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
    }
    
    private void Start()
    {
        StateMachine.Initialize(States.WalkState);
        
        SetRandomAttackRange();
    }

    private void SetRandomAttackRange()
    {
        _attackRange = Random.Range(MIN_ATTACK_RANGE, MAX_ATTACK_RANGE);
    }

    //TODO: Delete after testing
    protected override void Update()
    {
        base.Update();
        
        HandleDebug();
    }

    protected override IEnumerator ProcessDeath()
    {
        Collider.enabled = false;
        
        SetDeadState();
        
        float deathAnimationDuration = Animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationDuration);
        
        yield return StartCoroutine(FadeTo(0f, FADE_OUT_DURATION));
        
        Destroy(gameObject);
    }
    
    private IEnumerator FadeTo(float aValue, float aTime)
    {
        Color currentColor = _visualsRenderer.color;
        float alpha = currentColor.a;

        Color currentShadowColor = _shadow.color;
        float shadowAlpha = currentShadowColor.a;

        float fadeDelay = 1f;
        yield return new WaitForSeconds(fadeDelay);
        
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            var newColor = new Color(
                currentColor.r,
                currentColor.g,
                currentColor.b, 
                Mathf.Lerp(alpha,aValue,t));
            
            var newShadowColor = new Color(
                currentColor.r,
                currentColor.g,
                currentColor.b, 
                Mathf.Lerp(shadowAlpha,aValue,t));
            
            _visualsRenderer.color = newColor;
            _shadow.color = new Color(
                currentShadowColor.r, 
                currentShadowColor.g,
                currentShadowColor.b,
                newShadowColor.a);
            
            yield return null;
        }
    }

    protected override void SetDeadState()
    {
        if (StateMachine.CurrentState != States.DeadState)
        {
            StateMachine.ChangeState(States.DeadState);
        }
    }

    #endregion

    public void UpdateNextAttack()
    {
        _nextAttack = Time.time + _timeBetweenAttacks;
    }
    
    public bool AttackCooldownPassed() => Time.time > _nextAttack;

    public void TriggerAttackAnimation()
    {
        Animator.SetTrigger("Attack");
    }
    
    public bool SetTargetInAttackRange()
    {        
        Vector2 startPoint = OffsetRayStartingPoint();
        
        float distance = _attackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, _detectTargetLayerMask);

        if (hit.collider != null)
        {
            _target = hit.collider.GetComponent<Defender>();
            return true;
        }

        return false;
    }

    private Vector2 OffsetRayStartingPoint()
    {
        return transform.position + new Vector3(RANGE_START_OFFSET * _facingDirection.x, 0f, 0f);
    }

    #region Animation Event Methods

    private void Attack()
    {
        _target.TakeDamage(_damage);
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
        
        Vector2 startPoint = OffsetRayStartingPoint();
        
        float distance = _attackRange;
        Vector2 direction = _facingDirection;
        Vector2 endPoint = startPoint + distance * direction;
        
        Debug.DrawLine(startPoint, endPoint, _debugColor);
    }

    private void HandleDebug()
    {
        if (!SetTargetInAttackRange())
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