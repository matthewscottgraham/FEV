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
            
            var drawCardCommand = new DrawCardCommand(_commandController);
            _drawCardButton = CreateCardButton(drawCardCommand, null);
            _drawCardButton.clicked += drawCardCommand.Execute;
            _commandContainer.Insert(0, _drawCardButton);

            var confirmPlacementCommand = new PlaceFeatureCommand(_commandController);
            _confirmPlacementButton = CreateCardButton(confirmPlacementCommand, _commandContainer);
            _confirmPlacementButton.clicked += confirmPlacementCommand.Execute;
        }

        public void Redraw(Player player)
        {
            ClearCommandButtons();
            ClearStagingArea();

            _drawCardButton.visible = !_commandController.CurrentTurn.CardsDrawn;
            _confirmPlacementButton.visible = _commandController.CurrentTurn.CardsPlayed;
            
            DisplayPlayerName(player);
            DisplayPlayerCards(player);
            DisplayStagedCards();
        }

        private void DisplayPlayerCards(Player player)
        {
            _commandContainer.visible = true;
            
            if (player.Cards.Count == 0)
                return;
            
            foreach (var cardCommand in player.Cards)
            {
                var cardButton = CreateCardButton(cardCommand, _cardContainer);
                cardButton.clicked += cardCommand.Execute;
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
            
            foreach (var cardCommand in _commandController.StagedCards)
            {
                var cardButton = CreateCardButton(cardCommand, _stagedCardContainer);
                cardButton.clicked += ()=> _commandController.PlayerClaimsCard(cardCommand);
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
        
        private static Button CreateCardButton(ICommand command, VisualElement container)
        {
            var cardButton = new Button { text = command.Label };
            container?.Add(cardButton);
            return cardButton;
        }
    }
}