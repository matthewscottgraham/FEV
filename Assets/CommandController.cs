using System;

namespace FEV
{
    public class CommandController: IDisposable
    {
        private CommandView _view;
        private PlayerController _playerController;
        
        private DrawCardCommand _drawCardCommand;
        private PlaceFeatureCommand _placeFeatureCommand;
        
        public CommandController(CommandView view, PlayerController playerController)
        {
            _drawCardCommand = new();
            _placeFeatureCommand = new();
            _view = view;
            _playerController = playerController;

            _view.Initialize();
            _view.Redraw(_playerController.GetCurrentPlayer());
        }

        public void Dispose()
        {
            _view = null;
            _playerController = null;
            _drawCardCommand = null;
            _placeFeatureCommand = null;
        }
    }
}