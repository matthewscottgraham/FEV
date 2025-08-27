using Pegs;
using Players;
using System.Collections.Generic;

namespace Effects
{
    public class ScoreMultiplier : IEffect
    {
        private readonly int _multiplier;
        
        public ScoreMultiplier(int multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(Player player, List<Peg> pegs)
        {
            foreach (var peg in pegs)
            {
                peg.Score = _multiplier;
            }
        }
    }
}