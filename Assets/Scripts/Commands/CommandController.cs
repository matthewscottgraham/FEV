using System;
using System.Collections.Generic;
using Tiles;

namespace FEV
{
    public class CommandController: IDisposable
    {
        private MatchState _matchState;
        private CommandView _view;
        private PlayerController _playerController;
        private TileFactory _tileFactory;
        
        public CommandController(MatchState matchState, CommandView view, PlayerController playerController, TileFactory tileFactory)
        {
            _matchState = matchState;
            _view = view;
            _playerController = playerController;

            _view.Initialize(_matchState, this);
            _view.Redraw(_playerController.GetCurrentPlayer());
            
            _tileFactory = tileFactory;
            
            _matchState.OnTilePlayed += UpdateView;
            Tile.OnTileSelected += HandleTileSelected;
            DrawTileCommand.OnDrawTile += HandleDrawTile;
            _playerController.OnPlayerTurnStart += UpdateView;
        }

        public void Dispose()
        {
            _matchState.OnTilePlayed -= UpdateView;
            _playerController.OnPlayerTurnStart -= UpdateView;
            Tile.OnTileSelected -= HandleTileSelected;
            DrawTileCommand.OnDrawTile -= HandleDrawTile;
            
            _view = null;
            _playerController = null;
            _tileFactory = null;
            _matchState = null;
        }

        private void HandleDrawTile()
        {
            var tile = _tileFactory.DrawRandomTile();
            _playerController.GetCurrentPlayer().AddTile(tile);
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void UpdateView()
        {
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void HandleTileSelected(Tile tile)
        {
            _matchState.SelectedTile = tile;
        }
    }
}