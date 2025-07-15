using UnityEngine;
using Tiles;
using Pegs;

namespace Effects
{
    public interface IEffect
    {
        public void Apply(Vector2Int coordinates, Tile tile, Peg[,] board);
    }
}