using Pegs;
using Tiles;
using UnityEngine;

namespace Effects
{
    public class RadialGrow : IEffect
    {
        private readonly int _radius;

        public RadialGrow(int radius)
        {
            _radius = radius;
        }

        public void Apply(Vector2Int coordinates, Tile tile, Peg[,] board)
        {
            throw new System.NotImplementedException();
        }
    }
}