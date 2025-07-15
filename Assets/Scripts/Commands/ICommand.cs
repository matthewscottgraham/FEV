namespace Commands
{
    public interface ICommand
    {
        public string Label { get; }
        public void Execute();
        public void Destroy();
    }
}