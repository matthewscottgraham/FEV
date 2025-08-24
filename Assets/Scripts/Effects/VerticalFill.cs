using Pegs;
using Players;
using System.Collections.Generic;
using States;
using UnityEngine;

namespace Effects
{
    public class VerticalFill : IEffect
    {
        public void Apply(Player player, List<Peg> pegs)
        {
            foreach (var peg in pegs)
            {
                FillDown(peg.Coordinates, player);
                FillUp(peg.Coordinates, player);
            }
        }

        private void FillDown(Vector2Int coordinates, Player player)
        {
            coordinates.y -= 1;
            while (coordinates.y >= 0)
            {
                var peg = Board.Instance.GetPeg(coordinates);
                if (peg == null) break;
                if (peg.PegState is PegState.Claimed or PegState.Deactivated) break;
                
                peg.Claim(player);
                coordinates.y -= 1;
            }
        }

        private void FillUp(Vector2Int coordinates, Player player)
        {
            coordinates.y += 1;
            while (coordinates.y < Board.Instance.Height)
            {
                var peg = Board.Instance.GetPeg(coordinates);
                if (peg == null) break;
                if (peg.PegState is PegState.Claimed or PegState.Deactivated) break;
                
                peg.Claim(player);
                coordinates.y += 1;
            }
        }
    }
}