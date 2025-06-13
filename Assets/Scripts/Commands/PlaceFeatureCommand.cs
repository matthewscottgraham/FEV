namespace FEV
{
    internal class PlaceFeatureCommand : ICommand
    {
        public static System.Action OnConfirmPlaceFeature;
        public string Label => "Place Feature";
        
        private CommandController _commandController;
        
        public PlaceFeatureCommand(CommandController controller)
        {
            _commandController = controller;
        }
        
        public void Execute()
        {
            OnConfirmPlaceFeature?.Invoke();
        }
        
        public void Destroy()
        {
            // noop
        }
    }
}