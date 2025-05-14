namespace FEV
{
    internal class PlaceFeatureCommand : ICommand
    {
        public static System.Action OnConfirmPlaceFeature;
        public void Execute()
        {
            OnConfirmPlaceFeature?.Invoke();
        }
    }
}