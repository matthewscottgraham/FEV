namespace States
{
    public class EndTurnState : IState
    {
        public void EnterState()
        {
            StateMachine.NextState();
        }

        public void ExitState()
        {
            
        }
    }
}