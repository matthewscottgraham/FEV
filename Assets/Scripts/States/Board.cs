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

        public Peg GetPeg(Vector2Int coordinates)
        {
            return GetPeg(coordinates.x, coordinates.y);
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
                if (!scores.TryAdd(peg.Owner, peg.Score))
                {
                    scores[peg.Owner] += peg.Score;
                }
            }

            return scores;
        }
        
        public void FindAndClaimTrappedPegs()
        {
            Dictionary<Peg, Player> trappedPegs = new();

            for (var y = 0; y < Board.Instance.Height; y++)
            {
                for (var x = 0; x < Board.Instance.Width; x++)
                {
                    var currentPeg = Board.Instance.GetPeg(x, y);
                    var pegs = GetHorizontalPegLine(currentPeg);
                    pegs.AddRange(GetVerticalPegLine(currentPeg));
                    foreach (var peg in pegs)
                    {
                        trappedPegs.TryAdd(peg, currentPeg.Owner);
                    }
                }
            }

            ClaimPegs(trappedPegs);
        }

        private void ClaimPegs(Dictionary<Peg, Player> pegs)
        {
            foreach (var peg in pegs.Keys)
            {
                peg.Claim(pegs[peg]);
            }
        }

        private List<Peg> GetHorizontalPegLine(Peg startPeg)
        {
            var line = new List<Peg>();
            if (startPeg.PegState != PegState.Claimed) return line;

            line.Add(startPeg);
            var currentCoordinates = startPeg.Coordinates;
            currentCoordinates.x += 1;

            while (currentCoordinates.x < Width)
            {
                var currentPeg = GetPeg(currentCoordinates.x, currentCoordinates.y);
                if (currentPeg == null) break;
                if (currentPeg.PegState != PegState.Claimed) break;

                line.Add(currentPeg);
                currentCoordinates.x += 1;
            }

            return (IsLineValid(line)) ? line : new List<Peg>();
        }

        private List<Peg> GetVerticalPegLine(Peg startPeg)
        {
            var line = new List<Peg>();
            if (startPeg.PegState != PegState.Claimed) return line;

            line.Add(startPeg);
            var currentCoordinates = startPeg.Coordinates;
            currentCoordinates.y += 1;

            while (currentCoordinates.y < Height)
            {
                var currentPeg = GetPeg(currentCoordinates.x, currentCoordinates.y);
                if (currentPeg == null) break;
                if (currentPeg.PegState != PegState.Claimed) break;

                line.Add(currentPeg);
                currentCoordinates.y += 1;
            }

            return (IsLineValid(line)) ? line : new List<Peg>();
        }

        private bool IsLineValid(List<Peg> line)
        {
            if (line == null || line.Count < 3) return false;
            if (line[0].Owner != line[^1].Owner) return false;

            if (line.Count == 3) return true;
            for (var i = 2; i < line.Count - 1; i++)
            {
                if (line[i].Owner != line[1].Owner) return false;
            }

            return true;
        }
    }
}