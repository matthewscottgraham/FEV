using UnityEngine;

namespace FEV
{
    public class DrawCardCommand : ICommand
    {
        public System.Action OnComplete { get; set; }
        public string Label => "Draw card";
        
        public void Execute()
        {
            var card = GetRandomCard();
            Blackboard.Instance.CurrentPlayer.AddCard(card);
            OnComplete?.Invoke();
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