using System;
using System.Collections.Generic;
using Players;
using Tiles;

namespace FEV
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
            
            _matchConfiguration.OnTilePlayed += UpdateView;
            Tile.OnTileSelected += HandleTileSelected;
            DrawTileCommand.OnDrawTile += HandleDrawTile;
            _playerController.OnPlayerTurnStart += UpdateView;
        }

        public void Dispose()
        {
            _matchConfiguration.OnTilePlayed -= UpdateView;
            _playerController.OnPlayerTurnStart -= UpdateView;
            Tile.OnTileSelected -= HandleTileSelected;
            DrawTileCommand.OnDrawTile -= HandleDrawTile;
            
            _view = null;
            _playerController = null;
            _tileFactory = null;
            _matchConfiguration = null;
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
            _matchConfiguration.SelectedTile = tile;
        }
    }
}