using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootPoint;
    
    public void Fire()
    {
        Instantiate(_projectile, _shootPoint.transform.position, transform.rotation);
    }


}
