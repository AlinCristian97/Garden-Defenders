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

        [SerializeField] private RectTransform _rectTransform;

        private ISelectionManager _selectionManager;

        private void Awake()
        {
            // _rectTransform = GetComponent<RectTransform>();

            _button = GetComponent<Button>();
            _selectionManager = SelectionManager.Instance;
        }

        private void Start()
        {
            _defenderAvatarImage.sprite = Defender.Avatar;
            _defenderCostText.text = Defender.Cost.ToString();
        }

        //TODO: Don't use Update for this. Refactor!
        private void Update()
        {
            if (PauseControl.GameIsPaused)
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
            if (PauseControl.GameIsPaused) return;

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