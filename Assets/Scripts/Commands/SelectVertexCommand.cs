namespace FEV
{
    internal class SelectVertexCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Vertex);
        }
    }
}