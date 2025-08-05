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
            var dimensions = tile.Shape.GetShapeDimensions();
            var offset = dimensions / 2;
            
            if (coordinates.x - offset.x < 0) return false;
            if (coordinates.y - offset.y < 0) return false;
            if (coordinates.x + dimensions.x - 1 - offset.x > board.GetLength(0) -1) return false;
            if (coordinates.y + dimensions.y - 1 - offset.y > board.GetLength(1) -1) return false;
            
            return true;
        }
    }
}
