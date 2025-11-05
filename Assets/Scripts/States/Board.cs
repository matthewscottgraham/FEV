#nullable enable
using System;
using Pegs;
using Players;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class Board
    {
        private readonly int[,] _directions = { {-1, 0}, {1, 0}, {0, -1}, {0, 1}, {-1, -1}, {-1, 1}, {1, -1}, {1, 1} };
        private readonly Peg[,] _pegs;
        
        public static Board? Instance { get; private set; }
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
            catch
            {
                return null!;
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
        
        public void CaptureFlankedPegs()
        {
            var flankedPegs = new Dictionary<Player, HashSet<Peg>>();
            
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var currentPeg = GetPeg(x, y);
                    if (currentPeg == null || currentPeg.Owner == null) continue;
                    
                    for (var dir = 0; dir < 8; dir++)
                    {
                        var dy = _directions[dir, 0];
                        var dx = _directions[dir, 1];

                        var flankedLine = GetFlankedLine(x, y, dx, dy, currentPeg.Owner);

                        if (flankedLine == null) continue;
                        if (!flankedPegs.ContainsKey(currentPeg.Owner))
                            flankedPegs.Add(currentPeg.Owner, new HashSet<Peg>());
                        flankedPegs[currentPeg.Owner].Add(flankedLine);
                    }
                }
            }

            foreach (var player in flankedPegs.Keys)
            {
                foreach (var peg in flankedPegs[player])
                {
                    peg.Claim(player);
                }
            }
        }
        
        private List<Peg>? GetFlankedLine(int startX, int startY, int dx, int dy, Player currentPlayer)
        {
            var pegsInBetween = new List<Peg>();
            var x = startX + dx;
            var y = startY + dy;

            while (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                var peg = GetPeg(x, y);

                if (peg == null || peg.Owner == null)
                {
                    return null;
                }

                if (peg.Owner != currentPlayer)
                {
                    pegsInBetween.Add(peg);
                }
                else
                {
                    return pegsInBetween.Count > 0 ? pegsInBetween : null;
                }

                x += dx;
                y += dy;
            }

            return null;
        }

        
    }
}