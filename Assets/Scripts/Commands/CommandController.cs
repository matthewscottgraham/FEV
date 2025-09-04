using System;
using Commands.View;
using Pegs;
using Players;
using States;
using Tiles;

namespace Commands
{
    public class CommandController: IDisposable
    {
        private MatchConfiguration _matchConfiguration;
        
        private CommandView _view;
        private PegController _pegController;
        private PlayerController _playerController;
        private TileFactory _tileFactory;
        
        public CommandController(MatchConfiguration matchConfiguration, CommandView view, PlayerController playerController, TileFactory tileFactory, PegController pegController)
        {
            _matchConfiguration = matchConfiguration;
            _view = view;
            _pegController = pegController;
            _playerController = playerController;

            _view.Initialize(_matchConfiguration, this);
            _view.Redraw(_playerController.GetCurrentPlayer());
            
            _tileFactory = tileFactory;
            
            StateMachine.OnStateChanged += UpdateView;
            Tile.OnTileSelected += HandleTileSelected;
            StateMachine.OnStateChanged += HandleStateChange;

            _playerController.OnScoreUpdated += UpdateView;
            
            HandleStateChange();
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
            if (StateMachine.CurrentState.GetType() == typeof(StartTurnState))
            {
                var player = _playerController.GetCurrentPlayer();
                player.AddCommand(new DrawTileCommand(player));
            }

            if (StateMachine.CurrentState.GetType() == typeof(DrawTileState))
            {
                HandleDrawTile();
            }

            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void HandleDrawTile()
        {
            var player = _playerController.GetCurrentPlayer();
            
            player.AddCommand(_tileFactory.DrawRandomTile());
            player.AddCommand(new EndTurnCommand());

            foreach (var command in player.AvailableCommands)
            {
                if (command.GetType() != typeof(Tile)) continue;
                if (_pegController.CanTileBePlaced((Tile)command)) return;
            }

            StateMachine.EndGame();
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