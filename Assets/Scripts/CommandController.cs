using System;
using System.Collections.Generic;

namespace FEV
{
    public class CommandController: IDisposable
    {
        public List<ICommand> StagedCards { get; private set; } = new();
        
        private CommandView _view;
        private PlayerController _playerController;
        
        public CommandController(CommandView view, PlayerController playerController)
        {
            _view = view;
            _playerController = playerController;

            _view.Initialize(this);
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        public void Dispose()
        {
            _view = null;
            _playerController = null;
            StagedCards.Clear();
        }

        public void AddCards(List<ICommand> cards)
        {
            StagedCards = cards;
            _view.Redraw(_playerController.GetCurrentPlayer());
        }
    }
}