using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commands;
using FEV;
using States;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnScoreUpdated { get; set; }
        
        private MatchConfiguration _matchConfiguration;
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        private readonly Color[] _colors = new []{Color.blue, Color.red, Color.green, Color.yellow};
        
        public void Initialize(MatchConfiguration matchConfiguration)
        {
            _matchConfiguration = matchConfiguration;
            _players = new Player[_matchConfiguration.PlayerCount];
            
            var icons = Resources.LoadAll<Sprite>("Sprites/icons");
            
            for (int i = 0; i < _players.Length; ++i)
            {
                _players[i] = ScriptableObject.CreateInstance<Player>();
                _players[i].Initialize(i, _colors[i], icons[i]);
            }

            StateMachine.OnStateChanged += HandleStateChanged;
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

        public async void RemoveTile(Tiles.Tile tile, Player player)
        {
            player.RemoveTile(tile);

            if (GetCurrentPlayer().Tiles.Count > 0) return;
            
            await Task.Delay(TimeSpan.FromSeconds(2f));
        }

        public void NextPlayer()
        {
            _currentPlayerIndex++;
            _currentPlayerIndex %= _players.Length;
        }

        public void Dispose()
        {
            _players = null;
            StateMachine.OnStateChanged -= HandleStateChanged;
        }

        public Player GetPlayer(int playerIndex)
        {
            try { return _players[playerIndex]; }
            catch { return null; }
        }

        private void HandleStateChanged()
        {
            if (StateMachine.CurrentState.GetType() == typeof(EndTurnPhase))
                NextPlayer();
        }
    }
}