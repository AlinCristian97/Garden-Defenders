using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderVisualsOrderInLayerAdjuster : MonoBehaviour
{
    private Defender _parent;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        //TODO: Verify if this check is redundant
        if (transform.parent != null)
        {
            _parent = GetComponentInParent<Defender>();
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(_parent.Tile.transform.position.y) * -1;
    }
}