namespace FEV
{
    internal class PlaceEdgeFeatureCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Edge);
        }
    }
}