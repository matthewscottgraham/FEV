using System.Collections.Generic;
using Commands;
using Pegs;
using Players;
using Tiles;

namespace Effects
{
    public class DrawTileEffect : IEffect
    {
        public override string ToString()
        {
            return "Draw Additional Tile.";
        }
        
        public void Apply(Player player, List<Peg> pegs)
        {
            player.AddCommand(TileFactory.DrawRandomTile());
        }
    }
}