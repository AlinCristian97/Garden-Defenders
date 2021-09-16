using System.Collections.Generic;
using General.Patterns.Singleton;
using UnityEngine;

namespace UI
{
    public class DefenderSlotsDisplay : MonoBehaviour
    {
        [SerializeField] private DefenderSlot _defenderSlot;

        private void Start()
        {
            foreach (Defender defender in GameManager.Instance.ChosenDefendersList)
            {
                DefenderSlot defenderSlot = Instantiate(_defenderSlot, transform);

                defenderSlot.Defender = defender;
            }
        }
    }
}