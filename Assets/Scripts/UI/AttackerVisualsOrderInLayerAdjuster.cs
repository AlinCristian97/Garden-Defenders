using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerVisualsOrderInLayerAdjuster : MonoBehaviour
{
    private Attacker _parent;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        //TODO: Verify if this check is redundant
        if (transform.parent != null)
        {
            _parent = GetComponentInParent<Attacker>();
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //TODO: Check if it's right
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(_parent.transform.position.y) * -1;
    }
}
