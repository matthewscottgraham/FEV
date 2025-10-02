using System;
using Commands.View;
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
        
        public CommandController(MatchConfiguration matchConfiguration, CommandView view, PlayerController playerController)
        {
            _matchConfiguration = matchConfiguration;
            _view = view;
            _playerController = playerController;

            _view.Initialize(_matchConfiguration, this);
            _view.Redraw(_playerController.GetCurrentPlayer());
            
            StateMachine.OnStateChanged += UpdateView;
            StateMachine.OnStateChanged += HandleStateChange;
            Tile.OnTileSelected += HandleTileSelected;
            Player.OnCommandsModified += UpdateView;
            
            _playerController.OnScoreUpdated += UpdateView;
            
            HandleStateChange();
        }

        public void Dispose()
        {
            StateMachine.OnStateChanged -= UpdateView;
            StateMachine.OnStateChanged -= HandleStateChange;
            _playerController.OnScoreUpdated -= UpdateView;
            Tile.OnTileSelected -= HandleTileSelected;
            Player.OnCommandsModified -= UpdateView;
            
            _view = null;
            _playerController = null;
            _matchConfiguration = null;
        }

        private void HandleStateChange()
        {
            if (StateMachine.CurrentState.GetType() == typeof(StartTurnState))
            {
                var player = _playerController.GetCurrentPlayer();
                player.AddCommand(TileFactory.DrawRandomTile());
                player.AddCommand(new EndTurnCommand());
                StateMachine.PlayState();
            }

            _view.Redraw(_playerController.GetCurrentPlayer());
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