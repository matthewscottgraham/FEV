namespace FEV
{
    public class Tile : ICommand
    {
        public static System.Action<Tile> OnTileSelected;
        public Tile(TileShape shape)
        {
            Shape = shape;
        }

        public TileShape Shape { get; private set; }
        public string Label => Shape.name;

        public void Execute()
        {
            OnTileSelected?.Invoke(this);
        }

        public void Destroy()
        {
            Shape = null;
        }
    }
}