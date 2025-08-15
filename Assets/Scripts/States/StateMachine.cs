using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine: IDisposable
    {
        public static Action OnStateChanged;
        
        private static readonly Queue<State> StatesQueue = new();
        
        public static State CurrentState { get; private set; }
        
        public StateMachine()
        {
            StatesQueue.Enqueue(new StartTurnPhase());
            StatesQueue.Enqueue(new DrawTilePhase());
            StatesQueue.Enqueue(new PlaceTilePhase());
            StatesQueue.Enqueue(new PlaceAdditionalTilePhase());
            StatesQueue.Enqueue(new PlaceAdditionalTilePhase());
            StatesQueue.Enqueue(new NoMoreTilesPhase());
            StatesQueue.Enqueue(new EndTurnPhase());

            NextState();
        }
        
        public static void EndGame()
        {
            ChangeState(new EndGamePhase());
        }
        
        public static void EndTurn()
        {
            while (CurrentState.GetType() != typeof(StartTurnPhase))
            {
                NextState();
            }
        }
        
        public static void NextState()
        {
            var nextState = StatesQueue.Dequeue();
            ChangeState(nextState);
            StatesQueue.Enqueue(nextState);
            CurrentState.EnterState();
        }
        private static void ChangeState(State newState)
        {
            if (newState == null) return;
            CurrentState?.ExitState();
            CurrentState = newState;
            OnStateChanged?.Invoke();
            Debug.Log(CurrentState.ToString());
        }

        public void Dispose()
        {
            StatesQueue.Clear();
            CurrentState = null;
        }
    }
}