using Pegs;
using Players;
using System.Collections.Generic;
using States;
using UnityEngine;

namespace Effects
{
    public class HorizontalFill : IEffect
    {
        public void Apply(Player player, List<Peg> pegs)
        {
            foreach (var peg in pegs)
            {
                FillLeft(peg.Coordinates, player);
                FillRight(peg.Coordinates, player);
            }
        }

        private void FillLeft(Vector2Int coordinates, Player player)
        {
            coordinates.x -= 1;
            while (coordinates.x >= 0)
            {
                var peg = Board.Instance.GetPeg(coordinates);
                if (peg == null) break;
                if (peg.PegState is PegState.Claimed or PegState.Deactivated) break;
                
                peg.Claim(player);
                coordinates.x -= 1;
            }
        }

        private void FillRight(Vector2Int coordinates, Player player)
        {
            coordinates.x += 1;
            while (coordinates.x < Board.Instance.Width)
            {
                var peg = Board.Instance.GetPeg(coordinates);
                if (peg == null) break;
                if (peg.PegState is PegState.Claimed or PegState.Deactivated) break;
                
                peg.Claim(player);
                coordinates.x += 1;
            }
        }
    }
}