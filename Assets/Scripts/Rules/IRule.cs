using UnityEngine;
using Pegs;
using Tiles;

namespace Rules
{
    public interface IRule
    {
        public bool IsSatisfied(Vector2Int coordinates, Tile tile, Peg[,] board);
    }
}
