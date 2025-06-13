namespace FEV
{
    internal class SelectVertexCommand : ICommand
    {
        public string Label => "Vertex";
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Vertex);
        }
        
        public void Destroy()
        {
            Blackboard.Instance.CurrentPlayer.RemoveCard(this);
        }
    }
}