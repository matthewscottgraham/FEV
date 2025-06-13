using UnityEngine;
using UnityEngine.UIElements;

namespace FEV
{
    public class CommandView : MonoBehaviour
    {
        private CommandController _commandController;
        private UIDocument _uiDocument;
        
        private Label _playerLabel;
        private VisualElement _commandContainer;
        private VisualElement _cardContainer;
        private VisualElement _stagedCardContainer;

        private Button _drawCardButton;
        private Button _confirmPlacementButton;
        
        public void Initialize(CommandController commandController)
        {
            _commandController = commandController;
            
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _playerLabel = _uiDocument.rootVisualElement.Q<Label>("playerLabel");
            
            _commandContainer = _uiDocument.rootVisualElement.Q("commandContainer");
            _cardContainer = _uiDocument.rootVisualElement.Q("cardContainer");
            _stagedCardContainer = _uiDocument.rootVisualElement.Q("stagingArea");
            _drawCardButton = CreateStagedCardButton(new DrawCardCommand(_commandController), null);
            _commandContainer.Insert(0, _drawCardButton);
            
            _confirmPlacementButton = CreateStagedCardButton(new PlaceFeatureCommand(_commandController), null);
            _commandContainer.Add(_confirmPlacementButton);
        }

        public void Redraw(Player player)
        {
            ClearCommandButtons();
            ClearStagingArea();
            
            DisplayPlayerName(player);
            DisplayPlayerCards(player);
            DisplayStagedCards();
        }

        private void DisplayPlayerCards(Player player)
        {
            _commandContainer.visible = true;
            
            if (player.Cards.Count == 0)
                return;
            
            foreach (var card in player.Cards)
            {
                CreateStagedCardButton(card, _cardContainer);
            }
        }

        private void DisplayPlayerName(Player player)
        {
            _playerLabel.text = player.ToString();
            _playerLabel.style.color = player.Color;
        }

        private void DisplayStagedCards()
        {
            if (_commandController.StagedCards.Count == 0)
                return;
            
            foreach (var card in _commandController.StagedCards)
            {
                CreateStagedCardButton(card, _stagedCardContainer);
            }
        }

        private void ClearCommandButtons()
        {
            _cardContainer.Clear();
            _commandContainer.visible = false;
        }

        private void ClearStagingArea()
        {
            _stagedCardContainer.Clear();
        }
        
        private Button CreateStagedCardButton(ICommand command, VisualElement container)
        {
            var cardButton = new Button { text = command.Label };
            cardButton.clicked += command.Execute;
            container?.Add(cardButton);
            return cardButton;
        }
    }
}