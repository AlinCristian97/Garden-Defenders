using System;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;

namespace General
{
    public class Tile : MonoBehaviour
    {
        private bool IsEmpty => CurrentDefender == null;
        public Defender CurrentDefender => GetComponentInChildren<Defender>();

        private ISelectionManager _selectionManager;
        private IBuildManager _buildManager;

        private void Awake()
        {
            _selectionManager = SelectionManager.Instance;
            _buildManager = BuildManager.Instance;
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
                }
            }
        }
    }
}