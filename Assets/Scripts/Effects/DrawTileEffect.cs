using System.Collections.Generic;
using Commands;
using Pegs;
using Players;

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
            player.AddCommand(new DrawTileCommand(player));
        }
    }
}