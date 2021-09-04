using System;
using UnityEngine;

public class EnergyResource : MonoBehaviour
{
    [SerializeField] private int _energyAmount;
    [SerializeField] private float _expirationTime;

    private void Start()
    {
        //TODO: Add visual cue for when it's about to disappear
        Destroy(gameObject, _expirationTime);
    }

    private void OnMouseDown()
    {
        Debug.Log($"collected: {_energyAmount} Resource");
        Destroy(gameObject);
    }

}