using System.Collections.Generic;
using Players;
using Pegs;

namespace Effects
{
    public interface IEffect
    {
        public void Apply(Player player, List<Peg> pegs);
    }
}