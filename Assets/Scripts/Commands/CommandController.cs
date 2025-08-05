using System;
using Commands.View;
using FEV;
using Players;
using States;
using Tiles;

namespace Commands
{
    public class CommandController: IDisposable
    {
        private MatchConfiguration _matchConfiguration;
        
        private CommandView _view;
        private PlayerController _playerController;
        private TileFactory _tileFactory;
        
        public CommandController(MatchConfiguration matchConfiguration, CommandView view, PlayerController playerController, TileFactory tileFactory)
        {
            _matchConfiguration = matchConfiguration;
            _view = view;
            _playerController = playerController;

            _view.Initialize(_matchConfiguration, this);
            _view.Redraw(_playerController.GetCurrentPlayer());
            
            _tileFactory = tileFactory;
            
            StateMachine.OnStateChanged += UpdateView;
            Tile.OnTileSelected += HandleTileSelected;
            StateMachine.OnStateChanged += HandleStateChange;

            _playerController.OnScoreUpdated += UpdateView;
        }

        public void Dispose()
        {
            StateMachine.OnStateChanged -= UpdateView;
            _playerController.OnScoreUpdated -= UpdateView;
            Tile.OnTileSelected -= HandleTileSelected;
            StateMachine.OnStateChanged -= HandleStateChange;
            
            _view = null;
            _playerController = null;
            _tileFactory = null;
            _matchConfiguration = null;
        }

        private void HandleStateChange()
        {
            if (StateMachine.CurrentState.GetType() == typeof(DrawTilePhase))
                HandleDrawTiles();

            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void HandleDrawTiles()
        {
            var player = _playerController.GetCurrentPlayer();
            while (player.Tiles.Count < _matchConfiguration.MaxPlayerTileCount)
            {
                player.AddTile(_tileFactory.DrawRandomTile());
            }
        }

        private void UpdateView()
        {
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void HandleTileSelected(Tile tile)
        {
            _playerController.GetCurrentPlayer().SelectTile(tile);
            _view.Redraw(_playerController.GetCurrentPlayer());
        }
    }
}