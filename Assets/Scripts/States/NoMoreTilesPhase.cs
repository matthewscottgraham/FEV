namespace States
{
    public class NoMoreTilesPhase : State
    {
        public override bool CanDrawTiles  => false;
        public override bool CanEndTurn => true;
    }
}