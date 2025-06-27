namespace FEV
{
    internal class PlaceTileCommand : ICommand
    {
        public static System.Action OnConfirmPlaceTile;
        public string Label => "Place Feature";
        
        public void Execute()
        {
            OnConfirmPlaceTile?.Invoke();
        }
        
        public void Destroy()
        {
            // noop
        }
    }
}