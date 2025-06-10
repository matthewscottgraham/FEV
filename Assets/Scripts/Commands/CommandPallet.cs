using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FEV
{
    public class CommandPallet : MonoBehaviour
    {
        private Label _playerLabel;
        private VisualElement _commandPanel;
        private List<Button> _cards = new();
        private Button _drawCardButton;
        private Button _placeFeatureButton;
        
        private void OnEnable()
        {
            Blackboard.Instance.OnPlayerUpdate += HandleBlackboardUpdate;
            Player.OnCardsModified += HandleCardsModified;
        }

        private void OnDisable()
        {
            Blackboard.Instance.OnPlayerUpdate -= HandleBlackboardUpdate;
            Player.OnCardsModified -= HandleCardsModified;
        }

        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            
            _playerLabel = uiDocument.rootVisualElement.Q<Label>("playerLabel");
            _commandPanel = uiDocument.rootVisualElement.Q("commandPanel");
            
            var drawCardCommand = new DrawCardCommand();
            drawCardCommand.OnComplete += HandleCardDrawn;
            _drawCardButton = CreateCommandButton(drawCardCommand, false);
            
            _placeFeatureButton = CreateCommandButton(new PlaceFeatureCommand(), false);
        }

        private Button CreateCommandButton(ICommand command, bool isCard)
        {
            var button = new Button { text = command.Label };
            button.clicked += command.Execute;
            if (isCard)
            {
                _cards.Add(button);
            }
            _commandPanel.Add(button);
            return button;
        }

        private void HandleBlackboardUpdate()
        {
            if (_playerLabel == null) return;

            _playerLabel.text = $"Player {Blackboard.Instance.CurrentPlayer.Index}";
            _playerLabel.style.color = Blackboard.Instance.CurrentPlayer.Color;
            
            _drawCardButton.style.display = DisplayStyle.Flex;
            
            HandleCardsModified();
        }

        private void HandleCardsModified()
        {
            ClearCards();
            foreach (var card in Blackboard.Instance.CurrentPlayer.Cards)
            {
                CreateCommandButton(card, true);
            }
        }

        private void ClearCards()
        {
            foreach (var card in _cards)
            {
                card.RemoveFromHierarchy();
            }
            _cards.Clear();
        }

        private void HandleCardDrawn()
        {
            _drawCardButton.style.display = DisplayStyle.None;
        }
    }
}