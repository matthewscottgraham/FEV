namespace FEV
{
    internal class SelectFaceCommand : ICommand
    {
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Face);
        }
    }
}