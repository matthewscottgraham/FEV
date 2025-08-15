using UnityEngine;

namespace States
{
    public class EndGamePhase : State
    {
        public override bool CanDrawTiles  => false;
        public override bool CanEndTurn => false;

        public override void EnterState()
        {
            Debug.Log("Game Over");
        }
    }
}