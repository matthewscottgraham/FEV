using UnityEngine;
using UnityEngine.UIElements;

namespace FEV
{
    public class CommandView : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Label _playerLabel;
        private VisualElement _commandContainer;
        private VisualElement _cardContainer;

        private Button _drawCardButton;
        private Button _confirmPlacementButton;
        
        public void Initialize()
        {
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _playerLabel = _uiDocument.rootVisualElement.Q<Label>("playerLabel");
            
            _commandContainer = _uiDocument.rootVisualElement.Q("commandContainer");
            _cardContainer = _uiDocument.rootVisualElement.Q("cardContainer");

            _drawCardButton = CreateCommandButton(new DrawCardCommand(), false);
            _commandContainer.Insert(0, _drawCardButton);
            
            _confirmPlacementButton = CreateCommandButton(new PlaceFeatureCommand(), false);
            _commandContainer.Add(_confirmPlacementButton);
        }

        public void Redraw(Player player)
        {
            ClearCommandButtons();
            
            _playerLabel.text = player.ToString();
            _playerLabel.style.color = player.Color;
            
            _commandContainer.visible = true;

            foreach (var card in player.Cards)
            {
                CreateCommandButton(card, true);
            }
        }

        private void ClearCommandButtons()
        {
            _cardContainer.Clear();
            _commandContainer.visible = false;
        }
        
        private Button CreateCommandButton(ICommand command, bool isCard)
        {
            var button = new Button { text = command.Label };
            button.clicked += command.Execute;
            
            if (isCard)
            {
                _cardContainer.Add(button);
            }

            return button;
        }
    }
}