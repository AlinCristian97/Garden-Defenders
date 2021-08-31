using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //TODO: Does this variable make sense here?
    [field:SerializeField] public Sprite Avatar { get; private set; }
    
    [field:SerializeField] public int Cost { get; private set; }
    public Tile Tile => GetComponentInParent<Tile>();
    
    [Header("Shooting")]
    [SerializeField] private Projectile _projectile;
    [SerializeField] [Range(0.5f, 3f)] private float _timeBetweenShots = 1f;
    private float _nextShot;

    //Debug
    [SerializeField] private bool _enemyInLane;

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (CanShoot() && _enemyInLane)
        {
            _nextShot = Time.time + _timeBetweenShots;
            Instantiate(_projectile, transform.position, Quaternion.identity);
        }
    }

    private bool CanShoot() => Time.time > _nextShot;
}
