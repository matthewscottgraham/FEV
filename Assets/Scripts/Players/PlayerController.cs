using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commands;
using FEV;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnPlayerTurnStart { get; set; }
        public Action OnScoreUpdated { get; set; }
        
        private MatchConfiguration _matchConfiguration;
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        private readonly Color[] _colors = new []{Color.blue, Color.red, Color.green, Color.yellow};
        
        public void Initialize(MatchConfiguration matchConfiguration)
        {
            PlaceTileCommand.OnConfirmPlaceTiles += HandleTilePlaced;
            _matchConfiguration = matchConfiguration;
            _players = new Player[_matchConfiguration.PlayerCount];

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

        public void UpdateScores(Dictionary<Player, int> scores)
        {
            foreach (var player in _players)
            {
                player.SetScore(scores.ContainsKey(player) ? scores[player] : 0);
            }
            
            OnScoreUpdated?.Invoke();
        }

        private async void HandleTilePlaced()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1f));
            
            GetCurrentPlayer().RemoveTile(_matchConfiguration.SelectedTile);
            _matchConfiguration.SelectedTile = null;
            
            await Task.Delay(TimeSpan.FromSeconds(2f));
            
            _currentPlayerIndex++;
            _currentPlayerIndex %= _players.Length;
            OnPlayerTurnStart?.Invoke();
        }

        public void Dispose()
        {
            PlaceTileCommand.OnConfirmPlaceTiles -= HandleTilePlaced;
            _players = null;
        }

        public Player GetPlayer(int playerIndex)
        {
            try { return _players[playerIndex]; }
            catch { return null; }
        }
    }
}