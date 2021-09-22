using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace General.Patterns.Singleton
{
    public class WarnMessageManager : MonoBehaviour
    {
        #region Singleton

        private static WarnMessageManager _instance;
        
        public static WarnMessageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WarnMessageManager>();
                }
                
                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private GameObject _warnMessageCanvas;
        [SerializeField] private Transform _warnMessageContainer;
        [SerializeField] private GameObject _warningMessageGameObject;
        private TextMeshProUGUI _warnMessageText;
        private CanvasGroup _warnMessageCanvasGroup;

        private void Awake()
        {
            _warnMessageText = _warningMessageGameObject.GetComponentInChildren<TextMeshProUGUI>();
            _warnMessageCanvasGroup = _warningMessageGameObject.GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            DeactivateWarningMessageGameObject();
        }
        
        private void ActivateWarningMessageGameObject(string message)
        {
            _warnMessageText.text = message;
            _warnMessageCanvasGroup.alpha = 1;
            _warningMessageGameObject.SetActive(true);
        }
        
        public void DeactivateWarningMessageGameObject()
        {
            _warnMessageCanvasGroup.alpha = 0;
            _warningMessageGameObject.SetActive(false);
        }

        public void SpawnWarningMessage(string message, float delay)
        {
            StartCoroutine(SpawnWarningMessageCoroutine(message, delay));
        }

        private IEnumerator SpawnWarningMessageCoroutine(string message, float delay)
        {
            yield return new WaitForSeconds(delay);
            ActivateWarningMessageGameObject(message);
        }
    }
}
