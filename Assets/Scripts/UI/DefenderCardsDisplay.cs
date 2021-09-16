using System.Collections.Generic;
using General.Patterns.Singleton;
using UnityEngine;

namespace UI
{
    public class DefenderCardsDisplay : MonoBehaviour
    {
        [SerializeField] private DefenderCard _defenderCard;
        private readonly List<DefenderCard> _instantiatedDefenderCardsList = new List<DefenderCard>();

        private void Start()
        {
            InitializeAvailableCards();
        }

        private void InitializeAvailableCards()
        {
            foreach (Defender defender in GameManager.Instance.AvailableDefendersList)
            {
                DefenderCard defenderCard = Instantiate(_defenderCard, UIManager.Instance.AvailableCardsContainer);
                _instantiatedDefenderCardsList.Add(defenderCard);

                defenderCard.Defender = defender;
            }
        }

        public void UpdateChosenDefendersList()
        {
            GameManager.Instance.UpdateChosenDefendersList(GetChosenDefenders());
        }

        private List<Defender> GetChosenDefenders()
        {
            List<Defender> chosenDefenders = new List<Defender>();

            foreach (DefenderCard defenderCard in _instantiatedDefenderCardsList)
            {
                if (defenderCard.IsChosen)
                {
                    chosenDefenders.Add(defenderCard.Defender);
                }
            }

            return chosenDefenders;
        }
    }
}
