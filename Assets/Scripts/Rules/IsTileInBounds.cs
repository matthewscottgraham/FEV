using Pegs;
using States;
using Tiles;
using UnityEngine;

namespace Rules
{
    public class IsTileInBounds : IRule
    {
        
        public override string ToString()
        {
            return "Tile must be wholly in bounds.";
        }
        
        /// <summary>
        /// Checks if the tiles shape falls within the peg boards boundries
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile)
        {
            var dimensions = tile.Shape.GetShapeDimensions();
            var offset = dimensions / 2;
            
            if (coordinates.x - offset.x < 0) return false;
            if (coordinates.y - offset.y < 0) return false;
            if (coordinates.x + dimensions.x - 1 - offset.x > Board.Instance.Width -1) return false;
            if (coordinates.y + dimensions.y - 1 - offset.y > Board.Instance.Height -1) return false;
            
            return true;
        }
    }
}
