using Commands;
using DG.Tweening;
using Pegs;
using System;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Players
{
    public class Player : ScriptableObject
    {
        public static Action OnCommandsModified;
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
        }

        public void SetScore(int score)
        {
            Score = score;
        }
        
        public void AddCommand(ICommand command)
        {
            AvailableCommands.Add(command);
            SortCommands();
            OnCommandsModified?.Invoke();
        }

        public void RemoveCommand(ICommand command)
        {
            if (AvailableCommands.Contains(command))
                AvailableCommands.Remove(command);
            if (SelectedTile == command)
                SelectedTile = null;
            OnCommandsModified?.Invoke();
        }

        public void SelectTile(Tile tile)
        {
            if (AvailableCommands.Contains(tile)) SelectedTile = tile;
        }
        
        public override string ToString()
        {
            return $"Player: {Index + 1}";
        }

        // put add tile commands at front, strip out additional end tile commands
        private void SortCommands()
        {
            var sortedCommands = new List<ICommand>();
            ICommand endTurnCommand = null;
            foreach (var command in AvailableCommands)
            {
                if (command.GetType() == typeof(DrawTileCommand))
                    sortedCommands.Insert(0, command);
                else if (command.GetType() == typeof(EndTurnCommand))
                    endTurnCommand = command;
                else sortedCommands.Add(command);
            }
            if (endTurnCommand != null) sortedCommands.Add(endTurnCommand);
            AvailableCommands = sortedCommands;
        }
    }
}