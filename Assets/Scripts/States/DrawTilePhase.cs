namespace States
{
    public class DrawTilePhase : State
    {
        public override bool CanDrawTiles  => false;
        public override bool CanEndTurn => false;

        public override void EnterState()
        {
            StateMachine.NextState();
        }
    }
}