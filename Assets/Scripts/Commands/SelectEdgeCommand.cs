namespace FEV
{
    internal class SelectEdgeCommand : ICommand
    {
        public string Label => "Edge";
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Edge);
        }
        
        public void Destroy()
        {
            Blackboard.Instance.CurrentPlayer.RemoveCard(this);
        }
    }
}