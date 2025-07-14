using Pegs;
using Tiles;
using UnityEngine;

namespace Rules
{
    public class IsTileObstructed: IRule
    {
        /// <summary>
        /// Checks to see if the tile will intersect any claimed pegs
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile, Peg[,] board)
        {
            if (tile.CanTileIgnoreRule(GetType())) return true;
            
            var dimensions = tile.Shape.GetShapeDimensions();
            for (var y = 0; y < dimensions.y; y++)
            {
                for (var x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y)) continue;
                    if (board[coordinates.x + x, coordinates.y + y].Owner is not null) return false;
                }
            }
            return true;
        }
    }
}