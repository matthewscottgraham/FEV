using Pegs;
using Tiles;
using UnityEngine;

namespace Effects
{
    public class ScoreMultiplier : IEffect
    {
        private int _multiplier;
        
        public ScoreMultiplier(int multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(Vector2Int coordinates, Tile tile, Peg[,] board)
        {
            throw new System.NotImplementedException();
        }
    }
}