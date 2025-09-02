using System.Collections.Generic;
using DG.Tweening;
using Pegs;
using Tiles;
using Commands;
using UnityEngine;

namespace Players
{
    public class Player : ScriptableObject
    {
        public int Index { get; private set; }
        public PegStyle PegStyle { get; private set; }
        public bool IsHuman { get; private set; }
        public int Score { get; private set; } = 0;
        public List<ICommand> AvailableCommands { get; private set; } = new();
        public Tile SelectedTile { get; private set; }
        
        public void Initialize(int index, Color color, Sprite icon, bool isHuman = true)
        {
            Index = index;
            PegStyle = new PegStyle(color, icon, 0.8f, 0.5f, Ease.OutBack);
            IsHuman = isHuman;
        }

        public void EndTurn()
        {
            SelectedTile = null;
            AvailableCommands.Clear();
        }

        public void SetScore(int score)
        {
            Score = score;
        }
        
        public void AddCommand(ICommand command)
        {
            AvailableCommands.Add(command);
        }

        public void RemoveCommand(ICommand command)
        {
            if (AvailableCommands.Contains(command))
                AvailableCommands.Remove(command);
            if (SelectedTile == command)
                SelectedTile = null;
        }

        public void SelectTile(Tile tile)
        {
            if (AvailableCommands.Contains(tile)) SelectedTile = tile;
        }
        
        public override string ToString()
        {
            return $"Player: {Index + 1}";
        }
    }
}