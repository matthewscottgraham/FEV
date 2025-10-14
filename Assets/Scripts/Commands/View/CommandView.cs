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

        private VisualElement[] _playerInfoContainers;
        private Label[] _playerNameLabels;
        private Label[] _playerScoreLabels;
        
        private Label _effectLabel;
        private VisualElement _commandContainer;
        private VisualElement _tileContainer;

        private Button _menuButton;
        
        public void Initialize(MatchConfiguration matchConfiguration, CommandController commandController)
        {
            _matchConfiguration = matchConfiguration;
            _uiDocument = gameObject.GetComponent<UIDocument>();
            
            _playerInfoContainers = new VisualElement[_matchConfiguration.PlayerCount];
            _playerNameLabels = new Label[_matchConfiguration.PlayerCount];
            _playerScoreLabels = new Label[_matchConfiguration.PlayerCount];
            
            var playerInfoContainer = _uiDocument.rootVisualElement.Q<VisualElement>("PlayerInfo");
            for (var i = 0; i < _matchConfiguration.PlayerCount; i++)
            {
                CreatePlayerInfoDisplay(playerInfoContainer, i);
                if (i % 2 == 0) playerInfoContainer.AddSpacer();
            }
            
            _effectLabel = _uiDocument.rootVisualElement.Q<Label>("effectLabel");
            _commandContainer = _uiDocument.rootVisualElement.Q("commandContainer");
            _tileContainer = _uiDocument.rootVisualElement.Q("cardContainer");
            _menuButton = _uiDocument.rootVisualElement.Q<Button>("menuButton");
            _menuButton.clicked += HandleMenuButtonClicked;
        }

        public void Redraw(int currentPlayerIndex, Player[] allPlayers)
        {
            ClearCommandButtons();

            for (var i = 0; i < allPlayers.Length; i++)
            {
                DisplayPlayerName(allPlayers[i]);
                DisplayPlayerScore(allPlayers[i]);
                
                if (i == currentPlayerIndex) _playerInfoContainers[i].AddToClassList("active-player-info");
                else _playerInfoContainers[i].RemoveFromClassList("active-player-info");
            }

            DisplayPlayerTiles(allPlayers[currentPlayerIndex]);
        }

        private void CreatePlayerInfoDisplay(VisualElement parentContainer, int playerIndex)
        {
            var container = parentContainer.AddNew<VisualElement>();
            container.AddToClassList("info-container");
            _playerInfoContainers[playerIndex] = container;

            var playerNameLabel = container.AddNew<Label>("label-turn");
            playerNameLabel.text = "Player Name";
            _playerNameLabels[playerIndex] = playerNameLabel;
            
            var playerScoreLabel = container.AddNew<Label>("label-turn");
            playerScoreLabel.text = "Score";
            _playerScoreLabels[playerIndex] = playerScoreLabel;
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
            _playerNameLabels[player.Index].text = $"{player}";// ({suffix})";
            _playerNameLabels[player.Index].style.color = player.PegStyle.Color;
        }
        
        private void DisplayPlayerScore(Player player)
        {
            _playerScoreLabels[player.Index].text = "$ " + player.Score;
            _playerScoreLabels[player.Index].style.color = player.PegStyle.Color;
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
            button.AddToClassList("command-button");
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
            SceneLoader.LoadScene("scn_menu");
        }
    }
}