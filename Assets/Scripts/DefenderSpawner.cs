using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DefenderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _defender;

    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked");
        SpawnDefender();
    }

    private void SpawnDefender()
    {
        GameObject newDefender = Instantiate(_defender, transform.position, Quaternion.identity);
    }
}
