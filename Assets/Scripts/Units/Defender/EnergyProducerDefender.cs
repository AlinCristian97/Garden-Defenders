using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using General.ObjectPooling;
using General.Patterns.Singleton;
using General.Patterns.State.DefenderFSM;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnergyProducerDefender : Defender
{
    [Header("Delivering")]
    [SerializeField] private EnergyResource _energyResource;
    [SerializeField] private Transform _energyResourceSpawnPoint;
    [SerializeField] [Range(3f, 20f)] private float _timeBetweenDelivers = 1f;
    private float _nextDeliver;

    [SerializeField] private Sound[] _deliverSounds;
    
    public EnergyProducerDefenderStates States { get; private set; }

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        States = new EnergyProducerDefenderStates(this);
    }

    protected override void SetDeadState()
    {
        if (StateMachine.CurrentState != States.DeadState)
        {
            StateMachine.ChangeState(States.DeadState);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        UpdateNextDeliver();
    }

    protected void Start()
    {
        StateMachine.Initialize(States.IdleState);
        
        AudioManager.Instance.InitializeAudioSourceComponentsForArray(_deliverSounds, AudioManager.Instance.SoundEffectsGroup);
    }

    #endregion
    
    public void UpdateNextDeliver()
    {
        _nextDeliver = Time.time + _timeBetweenDelivers;
    }
    
    public bool DeliverCooldownPassed() => Time.time > _nextDeliver;
    
    public void TriggerDeliverAnimation()
    {
        Animator.SetTrigger("Deliver");
    }

    #region Animation Event Methods

    protected override void SetIdleState()
    {
        StateMachine.ChangeState(States.IdleState);
    }

    private void Deliver()
    {
        Vector3 position = _energyResourceSpawnPoint.position;

        #region Object Pooling

        ObjectPooler.Instance.SpawnFromPool(
            _energyResource.AliasIdentifier, 
            new Vector3(position.x, position.y,
                CameraInputLayer.PRIORITY_ENERGY_RESOURCE),
            Quaternion.identity);

        #endregion
        
    }
    
    private void PlayRandomDeliverSFX()
    {
        if (_deliverSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, _deliverSounds.Length);

            AudioManager.Instance.PlayOneShot(_deliverSounds, _deliverSounds[randomIndex].Name); 
        }
    }

    #endregion
}