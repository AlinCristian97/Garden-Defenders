using System;
using System.Collections;
using General.Patterns.Observer;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace General
{
    public class Tile : MonoBehaviour, IObserver
    {
        private bool IsEmpty => CurrentDefender == null;
        public Defender CurrentDefender => GetComponentInChildren<Defender>();
        [SerializeField] private GameObject _defenderPreviewPrefab;
        private GameObject _defenderPreview;

        private Animator _animator;
        
        private ISelectionManager _selectionManager;
        private IBuildManager _buildManager;

        private void OnEnable()
        {
            _selectionManager.AttachObserver(this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _selectionManager = SelectionManager.Instance;
            _buildManager = BuildManager.Instance;

            _defenderPreview = Instantiate(_defenderPreviewPrefab, transform);
        }

        private void Start()
        {
            _defenderPreview.SetActive(false);
        }

        private void OnDisable()
        {
            _selectionManager.DetachObserver(this);
        }

        private void OnMouseEnter()
        {
            if (_selectionManager.DefenderToBuild == null) return;
            if (!IsEmpty) return;

            var defenderSpriteRenderer = _defenderPreview.GetComponent<SpriteRenderer>();
            
            _defenderPreview.transform.position = transform.position - new Vector3(0f, 0.2f, 0f);
            defenderSpriteRenderer.color = new Color(255f, 255f, 255f, 0.5f);
            defenderSpriteRenderer.sprite = _selectionManager.DefenderToBuild.TilePreviewSprite;
            _defenderPreview.SetActive(true);
        }

        private void OnMouseExit()
        {
            _defenderPreview.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (PauseManager.Instance.GameIsPaused) return;
       
            if (!IsEmpty)
            {
                if (CurrentDefender == _selectionManager.DefenderToSell)
                {
                    _selectionManager.DeselectDefenderToSell();
                }
                else
                {
                    _selectionManager.SelectDefenderToSell(CurrentDefender);
                }
                AudioManager.Instance.PlayButtonClickSFX();
            }
            else
            {
                if (_selectionManager.DefenderToSell != null)
                {
                    _selectionManager.DeselectDefenderToSell();
                }
                else if (_selectionManager.DefenderToBuild != null)
                {
                    _buildManager.BuildDefender(transform.position, transform);
                    AudioManager.Instance.PlayButtonClickSFX();
                    OnMouseExit();
                }
            }
        }

        public void GetNotified()
        {
            if (IsEmpty)
            {
                _animator.SetBool(
                    "IsBuildDefenderSelected",
                    _selectionManager.DefenderToBuild != null);
            }
            else
            {
                _animator.SetBool("IsBuildDefenderSelected", false);
            }
        }
    }
}