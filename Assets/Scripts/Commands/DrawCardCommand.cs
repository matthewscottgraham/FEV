using System.Collections.Generic;
using UnityEngine;

namespace FEV
{
    public class DrawCardCommand : ICommand
    {
        public string Label => "Draw card";
        
        private CommandController _commandController;
        
        public DrawCardCommand(CommandController controller)
        {
            _commandController = controller;
        }
        
        public void Execute()
        {
            var cards = new List<ICommand>()
            {
                GetRandomCard(),
                GetRandomCard(),
                GetRandomCard()
            };
            _commandController.AddCards(cards);
        }

        public void Destroy()
        {
            // noop
        }

        private ICommand GetRandomCard()
        {
            var randomIndex = Random.Range(0, 3);
            return randomIndex switch
            {
                0 => new SelectVertexCommand(),
                1 => new SelectEdgeCommand(),
                2 => new SelectFaceCommand(),
                _ => null
            };
        }
    }
}