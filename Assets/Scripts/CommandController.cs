using System;
using System.Collections.Generic;

namespace FEV
{
    public class CommandController: IDisposable
    {
        private CommandView _view;
        private PlayerController _playerController;
        private TileFactory _tileFactory;
        
        public CommandController(CommandView view, PlayerController playerController, TileFactory tileFactory)
        {
            _view = view;
            _playerController = playerController;

            _view.Initialize(this);
            _view.Redraw(_playerController.GetCurrentPlayer());
            
            _tileFactory = tileFactory;

            MatchState.OnTurnUpdate += UpdateView;
        }

        public void Dispose()
        {
            _view = null;
            _playerController = null;
            _tileFactory = null;
            MatchState.OnTurnUpdate -= UpdateView;
        }

        public void AddTileToPlayer()
        {
            var tile = _tileFactory.DrawRandomTile();
            _playerController.GetCurrentPlayer().AddTile(tile);
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        private void UpdateView()
        {
            _view.Redraw(_playerController.GetCurrentPlayer());
        }
    }
}