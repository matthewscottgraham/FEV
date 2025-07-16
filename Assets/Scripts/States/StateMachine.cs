using System;
using System.Collections.Generic;

namespace States
{
    public class StateMachine: IDisposable
    {
        private readonly Queue<IState> _statesQueue = new();
        public IState CurrentState { get; private set; }
        
        public StateMachine()
        {
            _statesQueue.Enqueue(new StartTurnPhase());
            _statesQueue.Enqueue(new PlaceTilePhase());
            _statesQueue.Enqueue(new EndTurnPhase());

            NextState();
        }

        public void NextState()
        {
            var nextState = _statesQueue.Dequeue();
            ChangeState(nextState);
            _statesQueue.Enqueue(nextState);
        }
        
        private void ChangeState(IState newState)
        {
            if (newState == null) return;
            CurrentState?.OnExit();

            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public void Dispose()
        {
            _statesQueue.Clear();
            CurrentState = null;
        }
    }
}