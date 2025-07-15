namespace Commands
{
    internal class PlaceTileCommand : ICommand
    {
        public static System.Action OnConfirmPlaceTile;
        public string Label => "=";
        
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