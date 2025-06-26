using System.Collections.Generic;
using UnityEngine;

namespace FEV
{
    public class DrawTileCommand : ICommand
    {
        public string Label => "Draw card";
        
        private CommandController _commandController;
        
        public DrawTileCommand(CommandController controller)
        {
            _commandController = controller;
        }
        
        public void Execute()
        {
            _commandController.AddTileToPlayer();
        }

        public void Destroy()
        {
            _commandController = null;
        }
    }
}