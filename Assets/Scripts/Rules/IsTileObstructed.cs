using Pegs;
using States;
using Tiles;
using UnityEngine;

namespace Rules
{
    public class IsTileObstructed: IRule
    {
        public override string ToString()
        {
            return "Tile must be played on empty pegs.";
        }
        /// <summary>
        /// Checks to see if the tile will intersect any claimed or deactivated pegs
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile)
        {
            if (tile.CanTileIgnoreRule(GetType())) return true;
            
            var dimensions = tile.Shape.GetShapeDimensions();
            var offset = dimensions / 2;
            
            for (var y = 0; y < dimensions.y; y++)
            {
                for (var x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x, y)) continue;
                    var peg = Board.Instance.GetPeg(
                        coordinates.x + x - offset.x,
                        coordinates.y + y - offset.y);
                    
                    if (IsPegOwnedOrDeactivated(peg)) return false;
                }
            }
            return true;
        }
        
        public bool IsPegValid(Peg peg, Vector2Int tileCoordinates, Tile tile)
        {
            return !IsPegOwnedOrDeactivated(peg);
        }

        private bool IsPegOwnedOrDeactivated(Peg peg)
        {
            if (!peg) return true;
            return peg.PegState switch
            {
                PegState.Claimed or PegState.Deactivated => true,
                _ => false
            };
        }
    }
}