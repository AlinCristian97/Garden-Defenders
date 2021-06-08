using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;


    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }
}
