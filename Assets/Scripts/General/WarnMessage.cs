using System;
using General.Patterns.Singleton;
using UnityEngine;

namespace General
{
    public class WarnMessage : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource.Play();
        }

        public void DeactivateWarningMessage()
        {
            WarnMessageManager.Instance.DeactivateWarningMessageGameObject();
        }
    }
}