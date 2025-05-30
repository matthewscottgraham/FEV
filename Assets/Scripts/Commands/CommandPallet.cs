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
        private void OnEnable()
        {
            Blackboard.Instance.OnUpdate += HandleBlackboardUpdate;
            Player.OnCardsModified += HandleCardsModified;
        }

        private void OnDisable()
        {
            Blackboard.Instance.OnUpdate -= HandleBlackboardUpdate;
            Player.OnCardsModified -= HandleCardsModified;
        }

        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            
            _playerLabel = uiDocument.rootVisualElement.Q<Label>("playerLabel");
            _commandPanel = uiDocument.rootVisualElement.Q("commandPanel");
            
            CreateCommandButton(new DrawCardCommand(), false);
            CreateCommandButton(new PlaceFeatureCommand(), false);
        }

        private void CreateCommandButton(ICommand command, bool isCard)
        {
            var button = new Button { text = command.Label };
            button.clicked += command.Execute;
            if (isCard)
            {
                //button.clicked += command.Destroy;
                _cards.Add(button);
            }
            _commandPanel.Add(button);
        }

        private void HandleBlackboardUpdate()
        {
            if (_playerLabel == null) return;

            _playerLabel.text = $"Player {Blackboard.Instance.CurrentPlayer.Index}";
            _playerLabel.style.color = Blackboard.Instance.CurrentPlayer.Color;
            
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
    }
}