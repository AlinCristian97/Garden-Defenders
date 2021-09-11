using General;
using General.Patterns.Singleton;
using General.Patterns.Singleton.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefenderSlot : MonoBehaviour
    {
        public Defender Defender { get; set; }
        private Button _button;
    
        [SerializeField] private Image _defenderAvatarImage;
        [SerializeField] private Text _defenderCostText;
        
        private ISelectionManager _selectionManager;
        private IPauseManager _pauseManager;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _selectionManager = SelectionManager.Instance;
            _pauseManager = PauseManager.Instance;
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderCostText.text = Defender.Cost.ToString();
        }

        //TODO: Don't use Update for this. Refactor! (Observer)
        private void Update()
        {
            if (_pauseManager.GameIsPaused)
            {
                _button.interactable = false;
            }
            else
            {
                _button.interactable = true;
            }
        }

        public void SelectDefenderToBuild()
        {
            if (_pauseManager.GameIsPaused) return;

            if (_selectionManager.DefenderToBuild == Defender)
            {
                _selectionManager.DeselectDefenderToBuild();
            }
            else
            {
                _selectionManager.SelectDefenderToBuild(Defender);
            }
        }
    }
}