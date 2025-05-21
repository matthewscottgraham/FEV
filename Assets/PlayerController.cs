using System;
using UnityEngine;

namespace FEV
{
    public class PlayerController : MonoBehaviour
    {
        private Player[] _players;
        private void Start()
        {
            _players = new Player[2];
            
            _players[0] = ScriptableObject.CreateInstance<Player>();
            _players[0].Initialize(0, Color.blue);
            
            _players[1] = ScriptableObject.CreateInstance<Player>();
            _players[1].Initialize(1, Color.red);
            
            Blackboard.Instance.CurrentPlayer = _players[0];
        }

        private void OnEnable()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature += OnTurnCompleted;
        }

        private void OnDisable()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature -= OnTurnCompleted;
        }

        private void OnTurnCompleted()
        {
            var currentPlayerIndex = Blackboard.Instance.CurrentPlayer.Index;
            currentPlayerIndex++;
            currentPlayerIndex %= _players.Length;
            Blackboard.Instance.CurrentPlayer = _players[currentPlayerIndex];
        }
    }
}