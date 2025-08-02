namespace States
{
    public class PlaceTilePhase : State
    {
        public override bool CanDrawTiles  => false;
        public override bool CanEndTurn => false;
    }
}