using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    
    
    // [Tooltip("Our level timer in SECONDS")] [SerializeField] private float _levelTime = 10;
    //
    // private bool _triggeredLevelFinished = false;
    //
    // private Slider _slider;
    // private LevelController _levelController;
    //
    // private void Awake()
    // {
    //     _slider = GetComponent<Slider>();
    //     _levelController = FindObjectOfType<LevelController>();
    // }
    //
    // private void Update()
    // {
    //     if (_triggeredLevelFinished) return;
    //     _slider.value = Time.timeSinceLevelLoad / _levelTime;
    //     
    //     bool timerFinished = Time.timeSinceLevelLoad >= _levelTime;
    //     if (timerFinished)
    //     {
    //         Debug.Log("Level timer expired");
    //         _levelController.LevelTimerFinished();
    //         _triggeredLevelFinished = true;
    //     }
    // }
}
