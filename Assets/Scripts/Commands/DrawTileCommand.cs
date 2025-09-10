using States;
using Players;
using Tiles;

namespace Commands
{
    public class DrawTileCommand : ICommand
    {
        private readonly Player _player;
        public string Label => "+";
        
        public DrawTileCommand(Player player)
        {
            _player = player;
        }
        public void Execute()
        {
            _player.AddCommand(TileFactory.DrawRandomTile());
            _player.RemoveCommand(this);
        }

        public void Destroy()
        {
            // noop
        }
    }
}