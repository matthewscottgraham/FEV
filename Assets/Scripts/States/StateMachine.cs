using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine: IDisposable
    {
        public static Action OnStateChanged;
        
        private static readonly Queue<IState> StatesQueue = new();
        
        public static IState CurrentState { get; private set; }
        
        public StateMachine()
        {
            StatesQueue.Enqueue(new StartTurnState());
            StatesQueue.Enqueue(new DrawTileState());
            StatesQueue.Enqueue(new EndTurnState());

            NextState();
        }
        
        public static void EndGame()
        {
            ChangeState(new EndGameState());
        }
        
        public static void EndTurn()
        {
            while (CurrentState.GetType() != typeof(StartTurnState))
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
        private static void ChangeState(IState newState)
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