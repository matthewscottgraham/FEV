namespace FEV
{
    public class Tile : ICommand
    {
        public Tile(TileShape shape)
        {
            Shape = shape;
        }

        public TileShape Shape { get; private set; }
        public string Label => Shape.name;

        public void Execute()
        {
            MatchState.SelectedTile = this;
        }

        public void Destroy()
        {
            Shape = null;
        }
    }
}