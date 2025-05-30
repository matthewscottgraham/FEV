using UnityEngine;

namespace FEV
{
    public class DrawCardCommand : ICommand
    {
        public string Label => "Draw card";
        
        public void Execute()
        {
            var card = GetRandomCard();
            Blackboard.Instance.CurrentPlayer.AddCard(card);
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