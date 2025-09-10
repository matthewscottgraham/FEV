using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine: IDisposable
    {
        public static Action OnStateChanged;
        
        private static StateMachine _instance;
        private StateFactory _factory;
        public static IState CurrentState { get; private set; }
        
        public StateMachine()
        {
            _instance?.Dispose();
            _instance = this;
            _factory = new StateFactory();

            _instance.SetState(typeof(StartTurnState));
        }

        public static void EndGame()
        {
            _instance?.SetState(typeof(EndGameState));
        }

        public static void EndTurn()
        {
            _instance?.SetState(typeof(EndTurnState));
        }

        public static void PlayState()
        {
            _instance?.SetState(typeof(PlayState));
        }

        public static void StartTurnState()
        {
            _instance.SetState(typeof(StartTurnState));
        }

        public void Dispose()
        {
            CurrentState = null;
            _factory = null;
            _instance = null;
        }

        private void SetState(Type stateType)
        {
            SetState(_factory.GetState(stateType));
        }
        
        private void SetState(IState newState)
        {
            if (newState == null) return;
            CurrentState?.ExitState();
            CurrentState = newState;
            OnStateChanged?.Invoke();
            CurrentState.EnterState();
            Debug.Log(CurrentState.ToString());
        }
    }
}