using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootPoint;
    private AttackerSpawner _myLaneSpawner;
    private Animator _animator;

    private GameObject _projectileParent;
    private const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetLaneSpawner();

        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        _projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);

        if (!_projectileParent)
        {
            _projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            Debug.Log("shoot: pew pew");
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            Debug.Log("idle: sit and wait");
            _animator.SetBool("IsAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (AttackerSpawner spawner in spawners)
        {
            bool isCloseEnough = Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon;

            if (isCloseEnough)
            {
                _myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        return _myLaneSpawner.transform.childCount > 0;
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(_projectile, _shootPoint.transform.position, transform.rotation);

        newProjectile.transform.parent = _projectileParent.transform;
    }
}
