using Pegs;
using Players;
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
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsSatisfied(Vector2Int coordinates, Tile tile, Peg[,] board)
        {
            if (tile.CanTileIgnoreRule(GetType())) return true;
            
            var dimensions = tile.Shape.GetShapeDimensions();
            var currentPlayer = _playerController.GetCurrentPlayer();
            for (var y = 0; y < dimensions.y; y++)
            {
                for (var x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y)) continue;
                    if (AreAnyNeighbouringPegsOwnedByPlayer(
                        coordinates.x + x,
                        coordinates.y + y,
                        board,
                        currentPlayer
                        ))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool AreAnyNeighbouringPegsOwnedByPlayer(int x, int y, Peg[,] board, Player currentPlayer)
        {
             if (GetNeighbouringPeg(x - 1, y, board)?.Owner == currentPlayer) return true;
             if (GetNeighbouringPeg(x + 1, y, board)?.Owner == currentPlayer) return true;
             if (GetNeighbouringPeg(x, y - 1, board)?.Owner == currentPlayer) return true;
             if (GetNeighbouringPeg(x, y + 1, board)?.Owner == currentPlayer) return true;
            return false;
        }

        private Peg GetNeighbouringPeg(int x, int y, Peg[,] board)
        {
            try
            {
                return board[x, y];
            }
            catch
            {
                return null;
            }
        }
    }
}