using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Our level timer in SECONDS")] [SerializeField] private float _levelTime = 10;

    private void Update()
    {
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / _levelTime;
        
        bool timerFinished = Time.timeSinceLevelLoad >= _levelTime;
        if (timerFinished)
        {
            Debug.Log("Level timer expired");
        }
    }
}
