namespace FEV
{
    internal class PlaceFaceFeatureCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Face);
        }
    }
}