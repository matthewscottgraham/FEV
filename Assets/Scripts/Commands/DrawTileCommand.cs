namespace Commands
{
    public class DrawTileCommand : ICommand
    {
        public static System.Action OnDrawTile;
        public string Label => "+";
        
        public void Execute()
        {
            OnDrawTile?.Invoke();
        }

        public void Destroy()
        {
            // noop
        }
    }
}