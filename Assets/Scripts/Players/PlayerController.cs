using System;
using System.Threading.Tasks;
using UnityEngine;

namespace FEV
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnPlayerTurnStart { get; set; }
        
        private MatchState _matchState;
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        private Color[] _colors = new []{Color.blue, Color.red, Color.green, Color.yellow};
        public void Initialize(MatchState matchState)
        {
            PlaceTileCommand.OnConfirmPlaceTile += HandleTilePlaced;
            _matchState = matchState;
            _players = new Player[_matchState.playerCount];

            for (int i = 0; i < _players.Length; ++i)
            {
                _players[i] = ScriptableObject.CreateInstance<Player>();
                _players[i].Initialize(i, _colors[i]);
            }
            
            OnPlayerTurnStart?.Invoke();
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayerIndex];
        }

        private async void HandleTilePlaced()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1f));
            
            GetCurrentPlayer().RemoveTile(_matchState.SelectedTile);
            _matchState.SelectedTile = null;
            
            _currentPlayerIndex++;
            _currentPlayerIndex %= _players.Length;
            OnPlayerTurnStart?.Invoke();
        }

        public void Dispose()
        {
            PlaceTileCommand.OnConfirmPlaceTile -= HandleTilePlaced;
            _players = null;
        }
    }
}