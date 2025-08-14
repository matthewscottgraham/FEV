using Pegs;
using Players;
using System.Collections.Generic;
using States;
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

        public void Apply(Player player, List<Peg> pegs)
        {
            var affectedPegs = new HashSet<Peg>();
            foreach (var peg in pegs)
            {
                for (var y = -_radius; y <= _radius; y++)
                {
                    for (var x = -_radius; x <= _radius; x++)
                    {
                        affectedPegs.Add(GetNeighbour(peg, x, y));
                    }
                }
            }

            foreach (var peg in affectedPegs)
            {
                if (peg != null && peg.Owner == null)
                    peg.Claim(player);
            }
        }

        private Peg GetNeighbour(Peg peg, int x, int y)
        {
            var coordinates = peg.Coordinates + new Vector2Int(x, y);
            if (!Board.Instance.AreCoordinatesInBounds(coordinates)) return null;
            return Board.Instance.GetPeg(coordinates.x, coordinates.y);
        }
    }
}