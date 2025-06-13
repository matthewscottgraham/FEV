using System;
using UnityEngine;

namespace FEV
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnPlayerTurnStart { get; set; }
        
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        
        public void Initialize()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature += OnTurnCompleted;
            
            _players = new Player[2];
            
            _players[0] = ScriptableObject.CreateInstance<Player>();
            _players[0].Initialize(0, Color.blue);
            
            _players[1] = ScriptableObject.CreateInstance<Player>();
            _players[1].Initialize(1, Color.red);
            
            OnPlayerTurnStart?.Invoke();
            //Blackboard.Instance.SetCurrentPlayer(_players[0]);
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayerIndex];
        }

        private void OnTurnCompleted()
        {
            var currentPlayerIndex = Blackboard.Instance.CurrentPlayer.Index;
            currentPlayerIndex++;
            currentPlayerIndex %= _players.Length;
            //Blackboard.Instance.SetCurrentPlayer(_players[currentPlayerIndex]);
        }

        public void Dispose()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature -= OnTurnCompleted;
            _players = null;
        }
    }
}