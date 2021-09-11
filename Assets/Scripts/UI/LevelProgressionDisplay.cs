using System;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressionDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private float _duration;

        private void Start()
        {
            // _duration = SpawnManager.Instance.TimeBetweenWaves * ;
        }
    }
}
