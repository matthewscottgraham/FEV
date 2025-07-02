using Pegs;
using Tiles;
using UnityEngine;

namespace Rules
{
    public class IsTileInBounds : IRule
    {
        /// <summary>
        /// Checks if the tiles shape falls within the peg boards boundries
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile, Peg[,] board)
        {
            if (coordinates.x < 0 || coordinates.y < 0)
                return false;
            
            var tileDimensions = tile.Shape.GetShapeDimensions();
            
            if (coordinates.x + tileDimensions.x - 1 > board.GetLength(0) 
                || coordinates.y + tileDimensions.y - 1 > board.GetLength(1))
                return false;
            return true;
        }
    }
}
