namespace Commands
{
    internal class PlaceTileCommand : ICommand
    {
        public static System.Action OnConfirmPlaceTiles;
        public string Label => "=";
        
        public void Execute()
        {
            OnConfirmPlaceTiles?.Invoke();
        }
        
        public void Destroy()
        {
            // noop
        }
    }
}