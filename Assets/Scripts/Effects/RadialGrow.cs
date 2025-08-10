using Pegs;
using Players;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class RadialGrow : IEffect
    {
        private readonly int _radius;

        public RadialGrow(int radius)
        {
            _radius = radius;
        }

        public void Apply(Player player, List<Peg> pegs, Peg[,] board)
        {
            var affectedPegs = new HashSet<Peg>();
            foreach (var peg in pegs)
            {
                for (var y = -_radius; y <= _radius; y++)
                {
                    for (var x = -_radius; x <= _radius; x++)
                    {
                        affectedPegs.Add(GetNeighbour(peg, board, x, y));
                    }
                }
            }

            foreach (var peg in affectedPegs)
            {
                if (peg != null && peg.Owner == null)
                    peg.Claim(player);
            }
        }

        private Peg GetNeighbour(Peg peg, Peg[,] board, int x, int y)
        {
            var coordinates = peg.Coordinates + new Vector2Int(x, y);
            if (!AreCoordinatesInBounds(coordinates, board)) return null;
            return board[coordinates.x, coordinates.y];
        }
        
        public bool AreCoordinatesInBounds(Vector2Int coordinates, Peg[,] board)
        {
            if (coordinates.x < 0) return false;
            if (coordinates.y < 0) return false;
            if (coordinates.x > board.GetLength(0) -1) return false;
            if (coordinates.y > board.GetLength(1) -1) return false;
            
            return true;
        }
    }
}