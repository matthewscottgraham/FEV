using FEV;
using Players;
using Tiles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Commands
{
    public class CommandView : MonoBehaviour
    {
        private MatchConfiguration _matchConfiguration;
        
        private UIDocument _uiDocument;
        
        private Label _playerLabel;
        private Label _scoreLabel;
        private VisualElement _commandContainer;
        private VisualElement _tileContainer;

        private Button _menuButton;
        private Button _drawTileButton;
        private Button _confirmPlacementButton;
        
        public void Initialize(MatchConfiguration matchConfiguration, CommandController commandController)
        {
            _matchConfiguration = matchConfiguration;
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _playerLabel = _uiDocument.rootVisualElement.Q<Label>("playerLabel");
            _scoreLabel = _uiDocument.rootVisualElement.Q<Label>("playerScore");
            _commandContainer = _uiDocument.rootVisualElement.Q("commandContainer");
            _tileContainer = _uiDocument.rootVisualElement.Q("cardContainer");
            _menuButton = _uiDocument.rootVisualElement.Q<Button>("menuButton");
            _menuButton.clicked += HandleMenuButtonClicked;
            
            var drawTileCommand = new DrawTileCommand();
            _drawTileButton = CreateCommandButton(drawTileCommand, null);
            _commandContainer.Insert(0, _drawTileButton);

            var confirmPlacementCommand = new PlaceTileCommand();
            _confirmPlacementButton = CreateCommandButton(confirmPlacementCommand, _commandContainer);
        }

        public void Redraw(Player player)
        {
            ClearCommandButtons();

            _drawTileButton.visible = !_matchConfiguration.TilesDrawn;
            _confirmPlacementButton.visible = _matchConfiguration.TilesPlayed;
            
            DisplayPlayerName(player);
            DisplayPlayerScore(player);
            DisplayPlayerTiles(player);
        }

        private void DisplayPlayerTiles(Player player)
        {
            _commandContainer.visible = true;
            
            if (player.Tiles.Count == 0)
                return;
            
            foreach (var tile in player.Tiles)
            {
                CreateTileButton(tile, _tileContainer);
                //tileButton.clicked += tile.Execute;
            }
        }

        private void DisplayPlayerName(Player player)
        {
            var suffix = !player.IsHuman ? "Bot" : "Human";
            _playerLabel.text = $"{player}";// ({suffix})";
            _playerLabel.style.color = player.Color;
        }
        
        private void DisplayPlayerScore(Player player)
        {
            _scoreLabel.text = "$ " + player.Score;
            _scoreLabel.style.color = player.Color;
        }

        private void ClearCommandButtons()
        {
            _tileContainer.Clear();
            _commandContainer.visible = false;
        }
        
        private static Button CreateCommandButton(ICommand command, VisualElement container)
        {
            var button = new Button { text = command.Label };
            container?.Add(button);
            button.clicked += command.Execute;
            return button;
        }
        private static Button CreateTileButton(Tile tile, VisualElement container)
        {
            var button = new Button();
            button.Add(new Label(tile.GetIgnoredRuleLabel()));
            var image = new Image { image = tile.Shape.GetTexture() };
            image.style.width = 40;
            image.style.height = 40;
            button.Add(image);
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