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
            var tileDimensions = tile.Shape.GetShapeDimensions();
            
            if (coordinates.x < 0) return false;
            if (coordinates.y < 0) return false;
            if (coordinates.x + tileDimensions.x - 1 > board.GetLength(0) -1) return false;
            if (coordinates.y + tileDimensions.y - 1 > board.GetLength(1) -1) return false;
            
            return true;
        }
    }
}
