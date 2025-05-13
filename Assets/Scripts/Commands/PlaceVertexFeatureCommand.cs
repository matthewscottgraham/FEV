namespace FEV
{
    internal class PlaceVertexFeatureCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Vertex);
        }
    }
}