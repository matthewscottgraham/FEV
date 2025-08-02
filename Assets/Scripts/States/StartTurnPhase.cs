namespace States
{
    public class StartTurnPhase : State
    {
        public override bool CanDrawTiles  => true;
        public override bool CanEndTurn => false;
    }
}