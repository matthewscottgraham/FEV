using UnityEngine;

namespace States
{
    public class EndGameState : IState
    {
        public void EnterState()
        {
            Debug.Log("Game Over");
        }

        public void ExitState()
        {
            
        }
    }
}