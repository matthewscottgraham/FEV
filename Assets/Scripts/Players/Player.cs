using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Players
{
    public class Player : ScriptableObject
    {
        public int Index { get; private set; }
        public Color Color { get; private set; }
        public Sprite Icon { get; private set; }
        public bool IsHuman { get; private set; }
        public int Score { get; private set; } = 0;
        public List<Tile> Tiles { get; private set; } = new();
        public Tile SelectedTile { get; private set; }
        
        public void Initialize(int index, Color color, Sprite icon, bool isHuman = true)
        {
            Index = index;
            Color = color;
            Icon = icon;
            IsHuman = isHuman;
        }

        public void EndTurn()
        {
            SelectedTile = null;
            Tiles.Clear();
        }

        public void SetScore(int score)
        {
            Score = score;
        }
        public void AddTile(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void RemoveTile(Tile tile)
        {
            if (Tiles.Contains(tile))
                Tiles.Remove(tile);
            if (SelectedTile == tile)
                SelectedTile = null;
        }

        public void SelectTile(Tile tile)
        {
            if (Tiles.Contains(tile)) SelectedTile = tile;
        }
        
        public override string ToString()
        {
            return $"Player: {Index}";
        }
    }
}