namespace FEV
{
    internal class PlaceFeatureCommand : ICommand
    {
        public static System.Action OnConfirmPlaceFeature;
        public string Label => "Place Feature";
        
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