using States;

namespace Commands
{
    internal class EndTurnCommand : ICommand
    {
        public string Label => "END";
        
        public void Execute()
        {
            StateMachine.EndTurn();
        }
    }
}