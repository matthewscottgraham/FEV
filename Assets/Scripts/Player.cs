using System.Collections.Generic;
using UnityEngine;

namespace FEV
{
    public class Player : ScriptableObject
    {
        public int Index { get; private set; }
        public Color Color { get; private set; }
        public bool IsHuman { get; private set; }
        public List<Tile> Tiles { get; private set; } = new();
        public void Initialize(int index, Color color, bool isHuman = true)
        {
            Index = index;
            Color = color;
            IsHuman = isHuman;
        }

        public void AddTile(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void RemoveTile(Tile tile)
        {
            if (Tiles.Contains(tile))
                Tiles.Remove(tile);
        }
        
        public override string ToString()
        {
            return $"Player: {Index}";
        }
    }
}