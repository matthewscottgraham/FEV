namespace FEV
{
    internal class SelectEdgeCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Edge);
        }
    }
}