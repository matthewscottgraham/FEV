namespace States
{
    public class PlaceAdditionalTilePhase : State
    {
        public override bool CanDrawTiles  => false;
        public override bool CanEndTurn => true;
    }
}