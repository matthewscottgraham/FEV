using Pegs;
using Players;
using States;
using Tiles;
using UnityEngine;

namespace Rules
{
    public class IsTileAdjacentToOwnedPegs: IRule
    {
        private readonly PlayerController _playerController;
        
        public IsTileAdjacentToOwnedPegs(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        /// <summary>
        /// Checks if the tile will share a neighbouring peg owned by the player
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile)
        {
            if (tile.CanTileIgnoreRule(GetType())) return true;
            
            var dimensions = tile.Shape.GetShapeDimensions();
            var offset = dimensions / 2;
            var currentPlayer = _playerController.GetCurrentPlayer();
            for (var y = 0; y < dimensions.y; y++)
            {
                for (var x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y)) continue;
                    if (AreAnyNeighbouringPegsOwnedByPlayer(
                        coordinates.x + x - offset.x,
                        coordinates.y + y - offset.y,
                        currentPlayer
                        ))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool AreAnyNeighbouringPegsOwnedByPlayer(int x, int y, Player currentPlayer)
        {
             if (Board.Instance.GetPeg(x - 1, y)?.Owner == currentPlayer) return true;
             if (Board.Instance.GetPeg(x + 1, y)?.Owner == currentPlayer) return true;
             if (Board.Instance.GetPeg(x, y - 1)?.Owner == currentPlayer) return true;
             if (Board.Instance.GetPeg(x, y + 1)?.Owner == currentPlayer) return true;
            return false;
        }
    }
}