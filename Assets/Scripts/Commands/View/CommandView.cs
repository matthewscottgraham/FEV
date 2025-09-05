using System;
using Players;
using States;
using Tiles;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Commands.View
{
    public class CommandView : MonoBehaviour
    {
        private MatchConfiguration _matchConfiguration;
        
        private UIDocument _uiDocument;
        
        private Label _playerLabel;
        private Label _scoreLabel;
        private Label _effectLabel;
        private VisualElement _commandContainer;
        private VisualElement _tileContainer;

        private Button _menuButton;
        
        public void Initialize(MatchConfiguration matchConfiguration, CommandController commandController)
        {
            _matchConfiguration = matchConfiguration;
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _playerLabel = _uiDocument.rootVisualElement.Q<Label>("playerLabel");
            _scoreLabel = _uiDocument.rootVisualElement.Q<Label>("playerScore");
            _effectLabel = _uiDocument.rootVisualElement.Q<Label>("effectLabel");
            _commandContainer = _uiDocument.rootVisualElement.Q("commandContainer");
            _tileContainer = _uiDocument.rootVisualElement.Q("cardContainer");
            _menuButton = _uiDocument.rootVisualElement.Q<Button>("menuButton");
            _menuButton.clicked += HandleMenuButtonClicked;
        }

        public void Redraw(Player player)
        {
            ClearCommandButtons();
            
            DisplayPlayerName(player);
            DisplayPlayerScore(player);
            DisplayPlayerTiles(player);
        }

        private void DisplayPlayerTiles(Player player)
        {
            _commandContainer.SetVisibility(true);
            _effectLabel.text = "";
            
            if (player.AvailableCommands.Count == 0)
                return;
            
            foreach (var command in player.AvailableCommands)
            {
                var button = CreateCommandButton(command, player.PegStyle.Color, _tileContainer);
                if (player.SelectedTile == command)
                {
                    button.AddToClassList("selected");
                    _effectLabel.text = command.ToString();
                }
            }
        }

        private void DisplayPlayerName(Player player)
        {
            //var suffix = !player.IsHuman ? "Bot" : "Human";
            _playerLabel.text = $"{player}";// ({suffix})";
            _playerLabel.style.color = player.PegStyle.Color;
        }
        
        private void DisplayPlayerScore(Player player)
        {
            _scoreLabel.text = "$ " + player.Score;
            _scoreLabel.style.color = player.PegStyle.Color;
        }

        private void ClearCommandButtons()
        {
            _tileContainer.Clear();
            _commandContainer.SetVisibility(false);
        }
        
        private static Button CreateCommandButton(ICommand command, Color color, VisualElement container)
        {
            if (command.GetType() == typeof(Tile)) return CreateTileButton(command as Tile, container);
            var button = new Button { text = command.Label };
            container?.Add(button);
            button.clicked += command.Execute;
            return button;
        }
        private static Button CreateTileButton(Tile tile, VisualElement container)
        {
            var button = new TileElement(
                tile.Shape.GetTexture(),
                tile.HasEffect,
                tile.CanIgnoreAnyRule );
            container?.Add(button);
            button.clicked += tile.Execute;
            return button;
        }

        private void HandleMenuButtonClicked()
        {
            SceneLoader.LoadScene("scn_game");
        }
    }
}