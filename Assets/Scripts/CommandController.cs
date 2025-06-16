using System;
using System.Collections.Generic;

namespace FEV
{
    public class CommandController: IDisposable
    {
        public List<ICommand> StagedCards { get; private set; } = new();
        public Turn CurrentTurn { get; private set; } = new();
        
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

        public void StageCards(List<ICommand> cards)
        {
            StagedCards = cards;
            CurrentTurn.CardsDrawn = true;
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        public void PlayerClaimsCard(ICommand card)
        {
            _playerController.GetCurrentPlayer().AddCard(card);
            StagedCards.Clear();
            _view.Redraw(_playerController.GetCurrentPlayer());
        }
    }
}