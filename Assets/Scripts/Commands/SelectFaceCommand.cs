namespace FEV
{
    internal class SelectFaceCommand : ICommand
    {
        public string Label => "Face";
        public void Execute()
        {
            Blackboard.Instance.SetFeatureMode(FeatureMode.Face);
        }
        
        public void Destroy()
        {
            Blackboard.Instance.CurrentPlayer.RemoveCard(this);
        }
    }
}