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
            var tileShape = tile.Shape.GetShapeDimensions();
            for (var y = coordinates.y; y < coordinates.y + tileShape.y; y++)
            {
                for (var x = coordinates.x; x < coordinates.x + tileShape.x; x++)
                {
                    if(board[x, y] || board[x,y].Owner)
                        return false;
                }
            }
            return true;
        }
    }
}