using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputLayer : MonoBehaviour
{
    [SerializeField] private LayerMask _inputLayerMask;

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
