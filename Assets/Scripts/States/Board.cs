using System;
using Pegs;
using Players;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class Board
    {
        private readonly Peg[,] _pegs;
        public static Board Instance { get; private set; }
        public int Width => _pegs.GetLength(0);
        public int Height => _pegs.GetLength(1);
        public Board(Peg[,] pegs)
        {
            Instance = this;
            _pegs = pegs;
        }
        
        public bool AreCoordinatesInBounds(Vector2Int coordinates)
        {
            if (coordinates.x < 0) return false;
            if (coordinates.y < 0) return false;
            if (coordinates.x > Width -1) return false;
            if (coordinates.y > Height -1) return false;
            
            return true;
        }

        public Peg GetPeg(int x, int y)
        {
            try
            {
                return _pegs[x, y];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public Peg[] GetUnclaimedPegs()
        {
            var pegs = new List<Peg>();
            foreach (var peg in _pegs)
            {
                if (peg.Owner is null) pegs.Add(peg);
            }
            return pegs.ToArray();
        }
        
        public Dictionary<Player, int> CalculateScores()
        {
            var scores = new Dictionary<Player, int>();
            
            foreach (var peg in _pegs)
            {
                if (!peg.Owner) continue;
                if (!scores.TryAdd(peg.Owner, peg.GetScore()))
                {
                    scores[peg.Owner] += peg.GetScore();
                }
            }

            return scores;
        }
    }
}