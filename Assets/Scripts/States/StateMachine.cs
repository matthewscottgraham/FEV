using System;
using System.Threading.Tasks;
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
            _instance?.SetState(typeof(StartTurnState));
        }

        public void Dispose()
        {
            CurrentState = null;
            _factory = null;
            _instance = null;
        }

        private async void SetState(Type stateType)
        {
            await SetState(_factory.GetState(stateType));
        }
        
        private async Task SetState(IState newState)
        {
            if (newState == null) return;
            if (CurrentState != null) await CurrentState.ExitState();
            CurrentState = newState;
            OnStateChanged?.Invoke();
            await CurrentState.EnterState();
            Debug.Log(CurrentState.ToString());
        }
    }
}