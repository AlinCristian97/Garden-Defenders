using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputLayer : MonoBehaviour
{
    [SerializeField] private LayerMask _inputLayerMask;

    public const float PRIORITY_TILE = 0f;
    public const float PRIORITY_ENERGY_RESOURCE = -1f;
    
    private Camera _camera; 
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _camera.eventMask = _inputLayerMask;
    }
}
